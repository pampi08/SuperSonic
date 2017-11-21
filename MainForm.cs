using System;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing;
using AForge.Imaging;
using System.Text;
using Emgu.CV.Util;
using Emgu.CV.CvEnum;
using System.Diagnostics;
using System.Collections.Generic;
using AForge.Imaging.Filters;
using System.Drawing.Drawing2D;

namespace SS_OpenCV
{
    public partial class MainForm : Form
    {
        Image<Bgr, Byte> img = null; // working image
        Image<Bgr, Byte> imgUndo = null; // undo backup image - UNDO
        Image<Bgr, Byte> imgCopy = null; //same
        string title_bak = "";
        int mouseX, mouseY;
        bool mouseFlag = false;

        public MainForm()
        {
            InitializeComponent();
            title_bak = Text;
        }
        
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                img = new Image<Bgr, byte>(openFileDialog1.FileName);
                Text = title_bak + " [" +
                        openFileDialog1.FileName.Substring(openFileDialog1.FileName.LastIndexOf("\\") + 1) +
                        "]";
                imgUndo = img.Copy();
                ImageViewer.Image = img.Bitmap;
                ImageViewer.Refresh();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ImageViewer.Image.Save(saveFileDialog1.FileName);
            }
        }
        
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
        
        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (imgUndo == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor;
            img = imgUndo.Copy();

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor 
        }
        
        private void autoZoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // zoom
            if (autoZoomToolStripMenuItem.Checked)
            {
                ImageViewer.SizeMode = PictureBoxSizeMode.Zoom;
                ImageViewer.Dock = DockStyle.Fill;
            }
            else // with scroll bars
            {
                ImageViewer.Dock = DockStyle.None;
                ImageViewer.SizeMode = PictureBoxSizeMode.AutoSize;
            }
        }
        
        private void autoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AuthorsForm form = new AuthorsForm();
            form.ShowDialog();
        }

        private void negativeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.Negative(img);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor 
        }

        private void EvalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EvalForm eval = new EvalForm();
            eval.ShowDialog();
        }

        private void grayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.ConvertToGray(img);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor 
        }

        private void TranslationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            InputBox formx = new InputBox("Translation X?");
            formx.ShowDialog();
            int dx = Convert.ToInt32(formx.ValueTextBox.Text);
            InputBox formy = new InputBox("Translation Y?");
            formy.ShowDialog();
            int dy = Convert.ToInt32(formy.ValueTextBox.Text);

            ImageClass.Translation(img, imgUndo, dx, dy);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor 
        }
       
        private void RotationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            InputBox formr = new InputBox("Rotation (degrees)?");
            formr.ShowDialog();
            int degrees = Convert.ToInt32(formr.ValueTextBox.Text);
            float radian = degrees * (float)Math.PI / 180;

            ImageClass.Rotation(img, imgUndo, radian);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor 
        }
        private void biLinearToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            InputBox formr = new InputBox("Rotation (degrees)?");
            formr.ShowDialog();
            int degrees = Convert.ToInt32(formr.ValueTextBox.Text);
            float radian = degrees * (float)Math.PI / 180;

            ImageClass.Rotation_Bilinear(img, imgUndo, radian);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor 
        }

        private void ImageViewer_MouseClick(object sender, MouseEventArgs e)
        {
            if (mouseFlag) {
                mouseX = e.X; //get mouse
                mouseY = e.Y;

                mouseFlag = false; //unlock while(mouseFlag)
            }
        }
               
        private void nonUniformMeanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null)
                      return;
            Cursor = Cursors.WaitCursor;
            imgCopy = img.Copy();
            float[,] matrix = new float[3,3];
            
            InputForm inputForm = new InputForm();
            inputForm.b1.Value = 1;

            inputForm.ShowDialog();
            float b1 = (float)(inputForm.b1.Value);
            float b2 = (float)(inputForm.b2.Value);
            float b3 = (float)(inputForm.b3.Value);
            float b4 = (float)(inputForm.b4.Value);
            float b5 = (float)(inputForm.b5.Value);
            float b6 = (float)(inputForm.b6.Value);
            float b7 = (float)(inputForm.b7.Value);
            float b8 = (float)(inputForm.b8.Value);
            float b9 = (float)(inputForm.b9.Value);
            float matrixWeight = (float)(inputForm.weightB.Value);

            
            matrix[0,0] = b1;
            matrix[1,0] = b2;
            matrix[2,0] = b3;
            matrix[0,1] = b4;
            matrix[1,1] = b5;
            matrix[2,1] = b6;
            matrix[0,2] = b7;
            matrix[1,2] = b8;
            matrix[2,2] = b9;
            
            ImageClass.NonUniform(img, imgCopy, matrix, matrixWeight);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh();

            Cursor = Cursors.Default;

        }

        private void sobelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.Sobel(img, imgUndo);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor 
        }

        private void diferentiationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null)
                return;
            Cursor = Cursors.WaitCursor;
            imgCopy = img.Copy();

            ImageClass.Diferentiation(img, imgCopy);
            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default;

        }

        private void meanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.Mean(img, imgUndo);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor 
        }
        private void solutionBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.Mean_solutionB(img, imgUndo);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor 
        }

        private void solutionCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 
            int size = 0;
            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.Mean_solutionC(img, imgCopy, size);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor 
        }

        private void histogramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            int[] histogram1 = new int[255];
            histogram1[150] = 2000;
            histogram1[151] = 2000;
            histogram1[152] = 2000;
            int[] histogram = ImageClass.Histogram_Gray(img);
            Histogram histoChart = new Histogram("Gray Histogram", histogram , histogram1);
            //histoChart.chart1.Series[0].Points.DataBindY
            //            (histogram);
            //histoChart.chart1.Series[0].Points.DataBindY
            //            (histogram1);
            histoChart.ShowDialog();
            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor 
        }
        private void rGBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            int[] histogram = ImageClass.Histogram_RGB(img);
            ////Histogram histoChart = new Histogram("Gray Histogram", histogram);

            //histoChart.ShowDialog();

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor 
        }

        private void grayRGBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            int[] histogram = ImageClass.Histogram_All(img);
            //Histogram histoChart = new Histogram("Gray Histogram", histogram);

            //histoChart.ShowDialog();

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor 
        }


        private void bWToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            InputBox formbw = new InputBox("Threshold?");
            formbw.ShowDialog();
            string threshold = formbw.ValueTextBox.Text;
            bool success = int.TryParse(threshold, out int i);
            if (success && i >= 0 && i <= 255)
            {
                ImageClass.ConvertToBW(img, i);

                ImageViewer.Image = img.Bitmap;
                ImageViewer.Refresh(); // refresh image on the screen

                Cursor = Cursors.Default; // normal cursor 
            }
            else
            {
                MessageBox.Show("Value must be an integer from 0 to 255.", "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error); // for Error 
            }







        }

        private void bWOtsuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.ConvertToBW_Otsu(img);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor 
        }

        private void BrightnessContrasttoolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            InputBox formb = new InputBox("Brightness?");
            formb.ShowDialog();
            string brightness = formb.ValueTextBox.Text;
            bool successb = int.TryParse(brightness, out int i);
            if (successb && i >= 0 && i <= 255)
            {
                InputBox formc = new InputBox("Contrast?");
                formc.ShowDialog();
                string contrast = formc.ValueTextBox.Text;
                bool successc = int.TryParse(contrast, out int j);
                if (successc && j >= 0 && j <= 3)
                {
                    ImageClass.BrightContrast(img, i, j);

                    ImageViewer.Image = img.Bitmap;
                    ImageViewer.Refresh(); // refresh image on the screen

                    Cursor = Cursors.Default; // normal cursor 
                }
                else
                {
                    MessageBox.Show("Value for Contrast must be from 0 to 3.", "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error); // for Error 
                }
            }
            else
            {
                MessageBox.Show("Value for Brightness must be an integer from 0 to 255.", "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error); // for Error 
            }
        }

        private void zoomToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
            {
                return;
            }
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            InputBox formz = new InputBox("Zoom (factor)?");
            formz.ShowDialog();
            string factorZ = formz.ValueTextBox.Text;
            bool successc = float.TryParse(factorZ, out float j);
            if (successc && j > 0)
            {
                ImageClass.Scale(img, imgUndo, j);


                ImageViewer.Image = img.Bitmap;
                ImageViewer.Refresh(); // refresh image on the screen

                Cursor = Cursors.Default; // normal cursor 
            }
            else
            {
                MessageBox.Show("Value for Zoom must be a number bigger than zero.", "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error); // for Error 
            }

           
        }
        private void biLinearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
            {
                return;
            }
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            InputBox formz = new InputBox("Zoom (factor)?");
            formz.ShowDialog();
            string factorZ = formz.ValueTextBox.Text;
            bool successc = float.TryParse(factorZ, out float j);
            if (successc && j > 0)
            {
                ImageClass.Scale_Bilinear(img, imgUndo, j);


                ImageViewer.Image = img.Bitmap;
                ImageViewer.Refresh(); // refresh image on the screen

                Cursor = Cursors.Default; // normal cursor 
            }
            else
            {
                MessageBox.Show("Value for Zoom must be a number bigger than zero.", "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error); // for Error 
            }


        }

        private void mouseCenteredZoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
            {
                return;
            }
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            InputBox formz = new InputBox("Zoom (factor)?");
            formz.ShowDialog();
            string factorZ = formz.ValueTextBox.Text;
            bool success = float.TryParse(factorZ, out float j);
            if (success && j > 0)
            {
                mouseFlag = true;

                while (mouseFlag)
                {
                    Application.DoEvents();
                }
                          
                 ImageClass.Scale_point_xy(img, imgUndo, j, mouseX, mouseY);
                
                ImageViewer.Image = img.Bitmap;
                ImageViewer.Refresh(); // refresh image on the screen

                Cursor = Cursors.Default; // normal cursor 
            }
            else
            {
                MessageBox.Show("Value for Zoom must be a number bigger than zero.", "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error); // for Error 
            }
                 

        }
       
        private void ImageViewer_MouseClick_1(object sender, MouseEventArgs e)
        {
            if (mouseFlag)
            {
                mouseX = e.X;   //devolve as coordenadas do MouseClick
                mouseY = e.Y;

                mouseFlag = false;
            }
        }

        private void redChannelGrayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.RedChannel(img);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor 
        }

        private void robertsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgCopy = img.Copy();

            ImageClass.Roberts(img, imgCopy);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor 
        }

        private void chessRecognitionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();
            Color red = Color.Red;
            Pen p = new Pen(red);
            p.Width = 4;
            ImageClass.Chess_Recognition(img, imgUndo, out Rectangle BD_Location, out string Angle,
             out string[,] Pieces);
            Graphics g = Graphics.FromImage(img.Bitmap);
            //g.DrawRectangle(p, BD_Location);
            float angle = float.Parse(Angle,
      System.Globalization.CultureInfo.InvariantCulture);

            using (Matrix m = new Matrix())
            {
                m.RotateAt(angle, new PointF(BD_Location.Left ,
                                          BD_Location.Top ));
                g.Transform = m;
                g.DrawRectangle(p, BD_Location);
                g.ResetTransform();
            }


            ////Convert the image to grayscale and filter out the noise
            //Image<Gray, Byte> gray = img.Convert<Gray, Byte>().PyrDown().PyrUp();

            //Gray cannyThreshold = new Gray(180);
            //Gray cannyThresholdLinking = new Gray(120);
            //Gray circleAccumulatorThreshold = new Gray(120);

            //CircleF[] circles = gray.HoughCircles(
            //    cannyThreshold,
            //    circleAccumulatorThreshold,
            //    5.0, //Resolution of the accumulator used to detect centers of the circles
            //    10.0, //min distance 
            //    5, //min radius
            //    0 //max radius
            //    )[0]; //Get the circles from the first channel

            //Image<Gray, Byte> cannyEdges = gray.Canny(cannyThreshold.Intensity, cannyThresholdLinking.Intensity);
            //LineSegment2D[] lines = cannyEdges.HoughLinesBinary(
            //    1, //Distance resolution in pixel-related units
            //    Math.PI / 45.0, //Angle resolution measured in radians.
            //    20, //threshold
            //    30, //min Line width
            //   2 //gap between lines
            //    )[0]; //Get the lines from the first channel

            //#region Find rectangles

            //List<MCvBox2D> boxList = new List<MCvBox2D>();

            //using (MemStorage storage = new MemStorage()) //allocate storage for contour approximation
            //    for (Contour<System.Drawing.Point> contours = cannyEdges.FindContours(); contours != null; contours = contours.HNext)
            //    {
            //        Contour<System.Drawing.Point> currentContour = contours.ApproxPoly(contours.Perimeter * 0.05, storage);

            //        if (contours.Area > 250) //only consider contours with area greater than 250
            //        {

            //            if (currentContour.Total == 4) //The contour has 4 vertices.
            //            {
            //                #region determine if all the angles in the contour are within the range of [80, 100] degree
            //                bool isRectangle = true;
            //                System.Drawing.Point[] pts = currentContour.ToArray();
            //                LineSegment2D[] edges = PointCollection.PolyLine(pts, true);

            //                for (int i = 0; i < edges.Length; i++)
            //                {
            //                    double angle = Math.Abs(
            //                       edges[(i + 1) % edges.Length].GetExteriorAngleDegree(edges[i]));
            //                    if (angle < 80 || angle > 100)
            //                    {
            //                        isRectangle = false;
            //                        break;
            //                    }
            //                }
            //                #endregion

            //                if (isRectangle) boxList.Add(currentContour.GetMinAreaRect());
            //            }
            //        }
            //    }
            //#endregion


            //#region draw rectangles
            //Image<Bgr, Byte> RectangleImage = img.CopyBlank();

            //foreach (MCvBox2D box in boxList)
            //    RectangleImage.Draw(box, new Bgr(Color.DarkOrange), 2);

            //#endregion


            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor 
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.Sizable;
            WindowState = FormWindowState.Maximized;
        }

        private void medianToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.Median(img, imgUndo);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor 
        }
    }
}