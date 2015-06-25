using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    /// <summary>
    /// Class EdgeDetection computes detection of edges in bitmap. Edges are detected using gradient method. Gradient methods look 
    /// for the maximum and minimum in the first derivative of the image. For each pixel is applied convolution filter.
    /// After this step is applied edge thresholding. A value of threshold could be changed in GUI.
    /// </summary>
    public partial class EdgeDetection
    {
        /// <summary>
        /// Kernel for x-axis.
        /// </summary>
        private static int[,] gx;
        /// <summary>
        /// Kernel for y-axis.
        /// </summary>
        private static int[,] gy;

        /// <summary>
        /// Method for edge detection.
        /// </summary>
        /// <param name="original">original image</param>
        /// <param name="threshold">threshold value</param>
        /// <param name="operatorIndex">kernel index</param>
        /// <returns>image with detected edges</returns>
        public static Bitmap EdgeDetectionInImage(Bitmap original, byte threshold, int operatorIndex)
        {
            int width = original.Width;
            int height = original.Height;

            int BitsPerPixel = Image.GetPixelFormatSize(original.PixelFormat);
            int OneColorBits = BitsPerPixel / 8;

            BitmapData oriData = original.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, original.PixelFormat);
            int position;

            chooseMatrix(operatorIndex);

            Bitmap after = new Bitmap(width, height, original.PixelFormat);
            BitmapData afterData = after.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, after.PixelFormat);

            unsafe // unsafe mod, pointers are used
            {
                byte* ptr = (byte*)oriData.Scan0.ToPointer();
                byte* dst = (byte*)afterData.Scan0.ToPointer();

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

                        position = ((i * width + j) * OneColorBits);
                        if (newX * newX + newY * newY > threshold * threshold)
                            dst[position] = dst[position + 1] = dst[position + 2] = 255;
                        else
                            dst[position] = dst[position + 1] = dst[position + 2] = 0; 
                    }
                }

                original.UnlockBits(oriData);
                after.UnlockBits(afterData);
            }

            return after;
        }

        /// <summary>
        /// Method for choosing convolution matrix according given parameter. Zero for Sobel's matrix, 1 - Prewitt, 2 - Kirsch.
        /// </summary>
        /// <param name="operatorIndex">index of convolution matrix</param>
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

}
