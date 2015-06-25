using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    /// <summary>
    /// Class runs a graphical user interface for my project about edge relaxation in images.
    /// </summary>
    public partial class Form1 : Form
    {

        /// <summary>
        /// Original input image.
        /// </summary>
        Bitmap openedImage;
        /// <summary>
        /// Image after edge detection.
        /// </summary>
        Bitmap afterEdgeDection;

        /// <summary>
        /// Bitmap for canvas.
        /// </summary>
        Bitmap bitmapForDrawing;
        /// <summary>
        /// Index of convolution matrix.
        /// </summary>
        int operatorIndex;

        /// <summary>
        /// Axis. X or Y.
        /// </summary>
        char axis;
        /// <summary>
        /// First loop of edge relaxation?
        /// </summary>
        bool firstLoop;
        /// <summary>
        /// Image after relaxation, old version. This version is for "Step back".
        /// </summary>
        Bitmap oldRelax;
        /// <summary>
        /// Image after relaxation, new version.
        /// </summary>
        Bitmap newRelax;

        /// <summary>
        /// Max width of input image.
        /// </summary>
        int maxWidth = 512;
        /// <summary>
        /// Max height of input image.
        /// </summary>
        int maxHeight = 512;

        /// <summary>
        /// Initialize component.
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// For button "Open file".
        /// Method opens a dialog for opening new bmp file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Images (*.bmp)|*.bmp";
            DialogResult result = openFileDialog.ShowDialog();

            if(result == DialogResult.OK)
            {
                string filename = openFileDialog.FileName;
                //MessageBox.Show(filename);
                using (Bitmap imageCheck = (Bitmap)Bitmap.FromFile(filename))
                {
                    if (imageCheck.Width > maxWidth || imageCheck.Height > maxHeight)
                    {
                        MessageBox.Show("The image is too big.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (openedImage != null) 
                    {
                        openedImage.Dispose();
                        openedImage = null;
                    }

                    openedImage = (Bitmap)imageCheck.Clone(); // clone object of opened image into bitmap
                    invalidateNewImage(openedImage);
                    firstLoop = false;
                }
            }
        }

        /// <summary>
        /// For button "Edge detection".
        /// Method starts edge detection and shows a result in a new window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void edgesDetection(object sender, EventArgs e)
        {

            int imageThreshold = int.Parse(threshold.Text);

            if(imageThreshold < 0 || imageThreshold > 255)
            {
                MessageBox.Show("Threshold is unacceptable number. Range is (0, 255).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (openedImage == null)
            {
                MessageBox.Show("No image for edge detection.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            afterEdgeDection = EdgeDetection.EdgeDetectionInImage(openedImage, (byte) imageThreshold, operatorIndex);
            
            PictureBox P = new PictureBox();
            P.Height = afterEdgeDection.Height;
            P.Width = afterEdgeDection.Width;

            Form EdgeDetec = new Form
            {
                Text = "Edge detection",
                Size = new System.Drawing.Size(afterEdgeDection.Width+17, afterEdgeDection.Height+40),
                Location = new System.Drawing.Point(0, 0),
                Visible = true  
            };

            
            P.Image = afterEdgeDection;

            EdgeDetec.Controls.Add(P);
            EdgeDetec.Show();

        }

        /// <summary>
        /// For button "Original image".
        /// Method draws an original image on canvas.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void drawOriginalImage(object sender, EventArgs e)
        {
            invalidateNewImage(openedImage);
            firstLoop = false;
        }

        /// <summary>
        /// For button "Save file".
        /// Method opens a dialog for saving modified image as bitmap.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveImage(object sender, EventArgs e)
        {
            try
            {
                if(bitmapForDrawing != null)
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "Images (*.bmp)|*.bmp";
                    saveFileDialog.DefaultExt = "bmp";
                    saveFileDialog.ShowDialog();
                    string filePath = saveFileDialog.FileName;

                    if(filePath != null)
                    {
                        bitmapForDrawing.Save(filePath);
                        MessageBox.Show("Image saved.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Error while saving image.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        /// <summary>
        /// For bitton "Compute more steps".
        /// Method starts computing more steps of edge relaxation algorithm.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MultipleEdgeRelaxationInImage(object sender, EventArgs e)
        {
            if (firstLoop == false)
            {
                MessageBox.Show("No preprocessing for edge relaxation.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            try
            {
                int numberOfLoops = int.Parse(loops.Text);

                if (numberOfLoops <= 0)
                {
                    MessageBox.Show("Number of loops must be greater than zero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (newRelax != null)
                {
                    oldRelax = (Bitmap)newRelax.Clone();
                }

                newRelax = EdgeRelaxation.EdgeRelaxationLoop(numberOfLoops);
                invalidateNewImage(newRelax);
            }
            catch
            {
                MessageBox.Show("Count of loops must be a number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }

        /// <summary>
        /// For button "One more step".
        /// Method starts computing one loop of edge relaxation algorithm.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SingleEdgeRelaxationInImage(object sender, EventArgs e)
        {
            if (firstLoop == false)
            {
                MessageBox.Show("No preprocessing for edge relaxation.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (newRelax != null)
            {
                oldRelax = (Bitmap)newRelax.Clone();
            }

            newRelax = EdgeRelaxation.EdgeRelaxationLoop(1);
            invalidateNewImage(newRelax);
        }

        /// <summary>
        /// For button "Edge relaxation".
        /// Method starts edge relaxation algorithm. It is necessarily to run this method firts because it launches the firts step, 
        /// normalization of gradient values for each pixel.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartEdgeRelaxation(object sender, EventArgs e)
        {
            if (firstLoop == true)
            {
                MessageBox.Show("Load new image.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (openedImage == null)
            {
                MessageBox.Show("No image for edge relaxation.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if(newRelax != null) {
                oldRelax = (Bitmap)newRelax.Clone();
            }

            newRelax = EdgeRelaxation.EdgeRelaxationFirstLoop(openedImage, axis, operatorIndex);
            invalidateNewImage(newRelax);
            firstLoop = true;
        }

        /// <summary>
        /// For button "Step back".
        /// Method draws and 'old' image for steping back.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StepBack(object sender, EventArgs e)
        {
            if (oldRelax == null)
            {
                MessageBox.Show("There is no step to return to.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            invalidateNewImage(oldRelax);
        }

        // =========================== FORM CHANGES =============================

        /// <summary>
        /// Method repaint new image on canvas.
        /// </summary>
        /// <param name="newImage"></param>
        private void invalidateNewImage(Bitmap newImage)
        {
            bitmapForDrawing = newImage;
            this.Invalidate(); // invalidate form
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (bitmapForDrawing != null)
            {
                e.Graphics.DrawImage(bitmapForDrawing, 10, 10);
            }
        }

        /// <summary>
        /// Default values for form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            threshold.Text = "127";
            matrixCombo.SelectedIndex = 0;
            radioButton1.Checked = true;
            axis = 'x';
        }

        /// <summary>
        /// Convolution matrix combo. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void matrixCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(matrixCombo.SelectedIndex)
            {
                case 0:
                    operatorIndex = 0; // sobel
                    break;
                case 1:
                    operatorIndex = 1; // prewitt
                    break;
                case 2:
                    operatorIndex = 2; // kirsch
                    break;
            }
        }


        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                axis = 'x';
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                axis = 'y';
            }
        }


    }
}
