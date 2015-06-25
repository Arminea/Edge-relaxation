using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    /// <summary>
    /// Clas EdgeRelaxation computes relaxation of edges in image. 
    /// </summary>
    class EdgeRelaxation
    {
        /// <summary>
        /// Convolution mask for x axis.
        /// </summary>
        private static int[,] gx;
        /// <summary>
        /// Convolution mask for y axis.
        /// </summary>
        private static int[,] gy;

        /// <summary>
        /// Array of confidence of relax edges.
        /// </summary>
        private static double[,] relax;

        /// <summary>
        /// Maximal gradient magnitute.
        /// </summary>
        private static int maxValue;
        /// <summary>
        /// Is this our first loop?
        /// </summary>
        private static bool isFirstLoop = true;

        /// <summary>
        /// Delta parameter.
        /// </summary>
        private const double delta = 0.1;

        /// <summary>
        /// Method finds edges in image, computes a normalized gradient of each edge 
        /// and applies first loop of edge relaxation.
        /// </summary>
        /// <param name="image">original image</param>
        /// <param name="direction">direction, x-axis or y-axis</param>
        /// <param name="operatorIndex">index of convolution matrix</param>
        /// <returns></returns>
        public static Bitmap EdgeRelaxationFirstLoop(Bitmap image, char direction, int operatorIndex)
        {
            int[,] edges = FindEdges(image, direction, operatorIndex);
            double[,] normalized = NormalizeGradient(edges, maxValue);

            relax = ComputeConfidence(normalized, delta);

            return ToBitmap(relax);
        }

        /// <summary>
        /// Method runs an edge relaxation, one step or more.
        /// </summary>
        /// <param name="numberOfIteration">number of iteration</param>
        /// <param name="image">bitmap</param>
        /// <param name="direction">direction - x or y</param>
        /// <param name="operatorIndex">index of convolution operator</param>
        /// <returns>new bitmap</returns>
        public static Bitmap EdgeRelaxationLoop(int numberOfIteration)
        {

            for (int i = 0; i < numberOfIteration; i++)
            {
                relax = ComputeConfidence(relax, delta);
            }

            return ToBitmap(relax);
        }

        /// <summary>
        /// Method makes an bitmapimage from double array.
        /// </summary>
        /// <param name="relax">array of double</param>
        /// <returns>bitmap</returns>
        private static Bitmap ToBitmap(double[,] relax)
        {
            int width = relax.GetLength(1);
            int height = relax.GetLength(0);

            Bitmap image = new Bitmap(width, height);
            BitmapData bitmapData = image.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

            unsafe
            { 
                ColorARGB* startingPosition = (ColorARGB*)bitmapData.Scan0;
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        double color = relax[i, j];
                        byte rgb = (byte)(color * 255);

                        ColorARGB* position = startingPosition + j + i * width;
                        position->A = 255;
                        position->R = rgb;
                        position->G = rgb;
                        position->B = rgb;
                    }
                }
                image.UnlockBits(bitmapData);

            }

            return image;
        }

        /// <summary>
        /// Method computes new confidence of each edge.
        /// </summary>
        /// <param name="relax">matrix from previous loop</param>
        /// <param name="delta">delta parameter</param>
        /// <returns></returns>
        private static double[,] ComputeConfidence(double [,] relax, double delta)
        {
            double[,] newRelax = new double[relax.GetLength(0), relax.GetLength(1)];

            // option 1
            for (int i = 1; i < relax.GetLength(0) - 1; i++)
            {
                for (int j = 2; j < relax.GetLength(1) - 2; j++)
                {
                    int leftVertexType = VertexType(relax[i-1, j-1], relax[i, j-2], relax[i+1, j-1]);
                    int rightVertexType = VertexType(relax[i-1, j+1], relax[i, j+2], relax[i+1, j+1]);

                    newRelax[i, j] = ComputeNewEdgeConfidence(relax[i, j], delta, leftVertexType, rightVertexType);
                }
            } 

            // option 2
          /*  for (int i = 1; i < relax.GetLength(0) - 1; i++)
            {
                for (int j = 1; j < relax.GetLength(1) - 1; j++)
                {
                    int leftVertexType = VertexType(relax[i - 1, j - 1], relax[i, j - 1], relax[i + 1, j - 1]);
                    int rightVertexType = VertexType(relax[i - 1, j + 1], relax[i, j + 1], relax[i + 1, j + 1]);

                    newRelax[i, j] = ComputeNewEdgeConfidence(relax[i, j], delta, leftVertexType, rightVertexType);
                    //Console.WriteLine(newRelax[i, j]);
                }
            } */

                return newRelax;
        }

        /// <summary>
        /// Method normalizes gradient magnitude of each edge.
        /// </summary>
        /// <param name="edges">founded edges in image</param>
        /// <param name="maxValue">maximal gradient magnitude</param>
        /// <returns>normalized array</returns>
        private static double[,] NormalizeGradient(int[,] edges, int maxValue)
        {
            double[,] normalized = new double[edges.GetLength(0), edges.GetLength(1)];

            for (int i = 0; i < edges.GetLength(0); i++)
            {
                for (int j = 0; j < edges.GetLength(1); j++)
                {
                    normalized[i, j] = edges[i, j] / (double)maxValue ;
                    //Console.WriteLine(edges[i,j] + " " + normalized[i, j]);
                }
            }

            return normalized;
        }

        /// <summary>
        /// Method finds edges in image using convolution mask.
        /// </summary>
        /// <param name="original">original image</param>
        /// <param name="direction">edge direction - x or y</param>
        /// <param name="operatorIndex">index of convolution operator</param>
        /// <returns>array of gradient magnitudes for each edge (pixel)</returns>
        private static int[,] FindEdges(Bitmap original, char direction, int operatorIndex)
        {
            int width = original.Width;
            int height = original.Height;

            int BitsPerPixel = Image.GetPixelFormatSize(original.PixelFormat);
            int OneColorBits = BitsPerPixel / 8;

            BitmapData oriData = original.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, original.PixelFormat);
            int[,] directionMatrix = new int[height, width];

            maxValue = 0;

            chooseMatrix(operatorIndex);

            unsafe // unsafe mod, pointers are used
            {
                byte* ptr = (byte*)oriData.Scan0.ToPointer();

                for (int i = 1; i < height - 1; i++)
                {
                    for (int j = 1; j < width - 1; j++)
                    {
                        int newX = 0;
                        int newY = 0;

                        for (int m = 0; m < 3; m++)
                        {
                            for (int n = 0; n < 3; n++)
                            {
                                int I = i + m - 1;
                                int J = j + n - 1;
                                byte Current = *(ptr + (I * width + J) * OneColorBits);
                                newX += gx[m, n] * Current;
                                newY += gy[m, n] * Current;
                            }
                        }

                        if (direction == 'x')
                            directionMatrix[i, j] =  Math.Abs(newX); // axis x
                        else
                            directionMatrix[i, j] =  Math.Abs(newY); // axis y

                        if (directionMatrix[i, j] > maxValue) // maximal gradient magnitude
                            maxValue = directionMatrix[i, j];
                    }
                }
                
                original.UnlockBits(oriData);
            }

            return directionMatrix;
        }

        /// <summary>
        /// Mathod make decision about modifying the edge confidence based on edge type.
        /// </summary>
        /// <param name="oldConfidence">edge confidence from last loop</param>
        /// <param name="delta">delta constant</param>
        /// <param name="confLeft">type of left vertex</param>
        /// <param name="confRight">type of right vertex</param>
        /// <returns>confidence of edge</returns>
        private static double ComputeNewEdgeConfidence(double oldConfidence, double delta, int confLeft, int confRight)
        {
            double newConfidence = -1;
            char updateType = 'd'; // random default char

            if (confRight < confLeft) // swap for reverse values 3-0 => 0-3
            {
                int foo = confLeft;
                confLeft = confRight;
                confRight = foo;
            }

            switch (confLeft)
            {
                case 0:
                    if (confRight == 1)
                        updateType = 'l';
                    else
                        updateType = 'd';
                    break;
                case 1:
                    updateType = 'i';
                    break;
                case 2:
                    updateType = 'l';
                    break;
                case 3:
                    updateType = 'l';
                    break;
            }

            switch (updateType)
            {
                // increment
                case 'i': 
                    newConfidence = Math.Min(1, oldConfidence + delta);
                    break;
                // decrement
                case 'd': 
                    newConfidence = Math.Max(0, oldConfidence - delta);
                    break;
                // leave it
                case 'l': 
                    newConfidence = oldConfidence;
                    break;
                // leave it
                default:
                    newConfidence = oldConfidence;
                    break;
            }

            return newConfidence;
        }

        /// <summary>
        /// Constant which forces weak edges to become type zero.
        /// </summary>
        private const double q = 0.1;

        /// <summary>
        /// Method for computation of vertex type.
        /// </summary>
        /// <param name="a">upper edge</param>
        /// <param name="b">horizontal edge</param>
        /// <param name="c">lower edge</param>
        /// <returns>type of vertex</returns>
        private static int VertexType(double a, double b, double c)
        {
            double[] arrayForM = {a, b, c, q };
            double m = arrayForM.Max();

            double conf0 = (m-a) * (m-b) * (m-c);
            double conf1 = a * (m-b) * (m-c);
            double conf2 = a * b * (m-c);
            double conf3 = a * b * c;

            double[] arrayOfConf = { conf0, conf1, conf2, conf3 };

            double max = 0;
            int conf = -1;

            for (int i = 0; i < arrayOfConf.Length; i++)
            {
                if (arrayOfConf[i] > max)
                {
                    max = arrayOfConf[i];
                    conf = i;
                }
            }

            return conf;
        }

        /// <summary>
        /// Mathod chooses matrix for edge detection.
        /// </summary>
        /// <param name="operatorIndex">index of matrix</param>
        private static void chooseMatrix(int operatorIndex)
        {
            switch (operatorIndex)
            {
                case 0: // sobel
                    gx = Matrix.Sobel3x3_X;
                    gy = Matrix.Sobel3x3_Y;
                    break;
                case 1: // prewitt
                    gx = Matrix.Prewitt3x3_X;
                    gy = Matrix.Prewitt3x3_Y;
                    break;
                case 2: // kirsch
                    gx = Matrix.Kirsch3x3_X;
                    gy = Matrix.Kirsch3x3_Y;
                    break;
            }
        }
    }

    /// <summary>
    /// Auxiliary struct for transfering double array to bitmap image.
    /// </summary>
    public struct ColorARGB
    {
        public byte B;
        public byte G;
        public byte R;
        public byte A;

        public ColorARGB(Color color)
        {
            A = color.A;
            R = color.R;
            G = color.G;
            B = color.B;
        }

        public ColorARGB(byte a, byte r, byte g, byte b)
        {
            A = a;
            R = r;
            G = g;
            B = b;
        }

        public Color ToColor()
        {
            return Color.FromArgb(A, R, G, B);
        }
    }
}
