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
    public partial class Form1 : Form
    {

        Bitmap openedImage;
        Bitmap afterEdgeDection;

        Bitmap bitmapForDrawing;
        int operatorIndex;

        int maxWidth = 512;
        int maxHeight = 512;

        public Form1()
        {
            InitializeComponent();
        }


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
                }
            }
        }

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

            invalidateNewImage(afterEdgeDection);
        }


        private void drawOriginalImage(object sender, EventArgs e)
        {
            invalidateNewImage(openedImage);
        }


        // =========================== FORM CHANGES =============================

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

        private void Form1_Load(object sender, EventArgs e)
        {
            threshold.Text = "127";
            matrixCombo.SelectedIndex = 0;
        }

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


    }
}
