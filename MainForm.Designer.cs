using System;

namespace SS_OpenCV
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BrightnessContrasttoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.negativeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redChannelGrayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bWToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bWOtsuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.transformsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.translationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rotationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nearestNeighbourToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.biLinearToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.nearestNeighbourToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.biLinearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mouseCenteredZoomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filtersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.meanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.solutionAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.solutionBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.solutionCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nonUniformMeanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sobelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.diferentiationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.medianToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.robertsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoZoomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.histogramToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grayToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.rGBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grayRGBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.evalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chessRecognitionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ImageViewer = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ImageViewer)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "Images (*.png, *.bmp, *.jpg)|*.png;*.bmp;*.jpg";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.imageToolStripMenuItem,
            this.autoresToolStripMenuItem,
            this.evalToolStripMenuItem,
            this.chessRecognitionToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(9, 3, 0, 3);
            this.menuStrip1.Size = new System.Drawing.Size(1114, 35);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(50, 29);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(170, 30);
            this.openToolStripMenuItem.Text = "Open...";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(170, 30);
            this.saveToolStripMenuItem.Text = "Save As...";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(167, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(170, 30);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(54, 29);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(140, 30);
            this.undoToolStripMenuItem.Text = "Undo";
            this.undoToolStripMenuItem.Click += new System.EventHandler(this.undoToolStripMenuItem_Click);
            // 
            // imageToolStripMenuItem
            // 
            this.imageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BrightnessContrasttoolStripMenuItem,
            this.colorToolStripMenuItem,
            this.transformsToolStripMenuItem,
            this.filtersToolStripMenuItem,
            this.autoZoomToolStripMenuItem,
            this.histogramToolStripMenuItem});
            this.imageToolStripMenuItem.Name = "imageToolStripMenuItem";
            this.imageToolStripMenuItem.Size = new System.Drawing.Size(74, 29);
            this.imageToolStripMenuItem.Text = "Image";
            // 
            // BrightnessContrasttoolStripMenuItem
            // 
            this.BrightnessContrasttoolStripMenuItem.Name = "BrightnessContrasttoolStripMenuItem";
            this.BrightnessContrasttoolStripMenuItem.Size = new System.Drawing.Size(285, 30);
            this.BrightnessContrasttoolStripMenuItem.Text = "Brightness and Contrast";
            this.BrightnessContrasttoolStripMenuItem.Click += new System.EventHandler(this.BrightnessContrasttoolStripMenuItem_Click);
            // 
            // colorToolStripMenuItem
            // 
            this.colorToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.negativeToolStripMenuItem,
            this.grayToolStripMenuItem,
            this.redChannelGrayToolStripMenuItem,
            this.bWToolStripMenuItem,
            this.bWOtsuToolStripMenuItem});
            this.colorToolStripMenuItem.Name = "colorToolStripMenuItem";
            this.colorToolStripMenuItem.Size = new System.Drawing.Size(285, 30);
            this.colorToolStripMenuItem.Text = "Color";
            // 
            // negativeToolStripMenuItem
            // 
            this.negativeToolStripMenuItem.Name = "negativeToolStripMenuItem";
            this.negativeToolStripMenuItem.Size = new System.Drawing.Size(235, 30);
            this.negativeToolStripMenuItem.Text = "Negative";
            this.negativeToolStripMenuItem.Click += new System.EventHandler(this.negativeToolStripMenuItem_Click);
            // 
            // grayToolStripMenuItem
            // 
            this.grayToolStripMenuItem.Name = "grayToolStripMenuItem";
            this.grayToolStripMenuItem.Size = new System.Drawing.Size(235, 30);
            this.grayToolStripMenuItem.Text = "Gray";
            this.grayToolStripMenuItem.Click += new System.EventHandler(this.grayToolStripMenuItem_Click);
            // 
            // redChannelGrayToolStripMenuItem
            // 
            this.redChannelGrayToolStripMenuItem.Name = "redChannelGrayToolStripMenuItem";
            this.redChannelGrayToolStripMenuItem.Size = new System.Drawing.Size(235, 30);
            this.redChannelGrayToolStripMenuItem.Text = "Red Channel Gray";
            this.redChannelGrayToolStripMenuItem.Click += new System.EventHandler(this.redChannelGrayToolStripMenuItem_Click);
            // 
            // bWToolStripMenuItem
            // 
            this.bWToolStripMenuItem.Name = "bWToolStripMenuItem";
            this.bWToolStripMenuItem.Size = new System.Drawing.Size(235, 30);
            this.bWToolStripMenuItem.Text = "B&W";
            this.bWToolStripMenuItem.Click += new System.EventHandler(this.bWToolStripMenuItem_Click);
            // 
            // bWOtsuToolStripMenuItem
            // 
            this.bWOtsuToolStripMenuItem.Name = "bWOtsuToolStripMenuItem";
            this.bWOtsuToolStripMenuItem.Size = new System.Drawing.Size(235, 30);
            this.bWOtsuToolStripMenuItem.Text = "BW - Otsu";
            this.bWOtsuToolStripMenuItem.Click += new System.EventHandler(this.bWOtsuToolStripMenuItem_Click);
            // 
            // transformsToolStripMenuItem
            // 
            this.transformsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.translationToolStripMenuItem,
            this.rotationToolStripMenuItem,
            this.zoomToolStripMenuItem1,
            this.mouseCenteredZoomToolStripMenuItem});
            this.transformsToolStripMenuItem.Name = "transformsToolStripMenuItem";
            this.transformsToolStripMenuItem.Size = new System.Drawing.Size(285, 30);
            this.transformsToolStripMenuItem.Text = "Transforms";
            // 
            // translationToolStripMenuItem
            // 
            this.translationToolStripMenuItem.Name = "translationToolStripMenuItem";
            this.translationToolStripMenuItem.Size = new System.Drawing.Size(279, 30);
            this.translationToolStripMenuItem.Text = "Translation";
            this.translationToolStripMenuItem.Click += new System.EventHandler(this.TranslationToolStripMenuItem_Click);
            // 
            // rotationToolStripMenuItem
            // 
            this.rotationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nearestNeighbourToolStripMenuItem1,
            this.biLinearToolStripMenuItem1});
            this.rotationToolStripMenuItem.Name = "rotationToolStripMenuItem";
            this.rotationToolStripMenuItem.Size = new System.Drawing.Size(279, 30);
            this.rotationToolStripMenuItem.Text = "Rotation";
            // 
            // nearestNeighbourToolStripMenuItem1
            // 
            this.nearestNeighbourToolStripMenuItem1.Name = "nearestNeighbourToolStripMenuItem1";
            this.nearestNeighbourToolStripMenuItem1.Size = new System.Drawing.Size(248, 30);
            this.nearestNeighbourToolStripMenuItem1.Text = "Nearest-Neighbour";
            this.nearestNeighbourToolStripMenuItem1.Click += new System.EventHandler(this.RotationToolStripMenuItem_Click);
            // 
            // biLinearToolStripMenuItem1
            // 
            this.biLinearToolStripMenuItem1.Name = "biLinearToolStripMenuItem1";
            this.biLinearToolStripMenuItem1.Size = new System.Drawing.Size(248, 30);
            this.biLinearToolStripMenuItem1.Text = "BiLinear";
            this.biLinearToolStripMenuItem1.Click += new System.EventHandler(this.biLinearToolStripMenuItem1_Click);
            // 
            // zoomToolStripMenuItem1
            // 
            this.zoomToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nearestNeighbourToolStripMenuItem,
            this.biLinearToolStripMenuItem});
            this.zoomToolStripMenuItem1.Name = "zoomToolStripMenuItem1";
            this.zoomToolStripMenuItem1.Size = new System.Drawing.Size(279, 30);
            this.zoomToolStripMenuItem1.Text = "Zoom";
            // 
            // nearestNeighbourToolStripMenuItem
            // 
            this.nearestNeighbourToolStripMenuItem.Name = "nearestNeighbourToolStripMenuItem";
            this.nearestNeighbourToolStripMenuItem.Size = new System.Drawing.Size(248, 30);
            this.nearestNeighbourToolStripMenuItem.Text = "Nearest-Neighbour";
            this.nearestNeighbourToolStripMenuItem.Click += new System.EventHandler(this.zoomToolStripMenuItem1_Click);
            // 
            // biLinearToolStripMenuItem
            // 
            this.biLinearToolStripMenuItem.Name = "biLinearToolStripMenuItem";
            this.biLinearToolStripMenuItem.Size = new System.Drawing.Size(248, 30);
            this.biLinearToolStripMenuItem.Text = "BiLinear";
            this.biLinearToolStripMenuItem.Click += new System.EventHandler(this.biLinearToolStripMenuItem_Click);
            // 
            // mouseCenteredZoomToolStripMenuItem
            // 
            this.mouseCenteredZoomToolStripMenuItem.Name = "mouseCenteredZoomToolStripMenuItem";
            this.mouseCenteredZoomToolStripMenuItem.Size = new System.Drawing.Size(279, 30);
            this.mouseCenteredZoomToolStripMenuItem.Text = "Mouse Centered Zoom";
            this.mouseCenteredZoomToolStripMenuItem.Click += new System.EventHandler(this.mouseCenteredZoomToolStripMenuItem_Click);
            // 
            // filtersToolStripMenuItem
            // 
            this.filtersToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.meanToolStripMenuItem,
            this.nonUniformMeanToolStripMenuItem,
            this.sobelToolStripMenuItem,
            this.diferentiationToolStripMenuItem,
            this.medianToolStripMenuItem,
            this.robertsToolStripMenuItem});
            this.filtersToolStripMenuItem.Name = "filtersToolStripMenuItem";
            this.filtersToolStripMenuItem.Size = new System.Drawing.Size(285, 30);
            this.filtersToolStripMenuItem.Text = "Filters";
            // 
            // meanToolStripMenuItem
            // 
            this.meanToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.solutionAToolStripMenuItem,
            this.solutionBToolStripMenuItem,
            this.solutionCToolStripMenuItem});
            this.meanToolStripMenuItem.Name = "meanToolStripMenuItem";
            this.meanToolStripMenuItem.Size = new System.Drawing.Size(297, 30);
            this.meanToolStripMenuItem.Text = "Mean";
            // 
            // solutionAToolStripMenuItem
            // 
            this.solutionAToolStripMenuItem.Name = "solutionAToolStripMenuItem";
            this.solutionAToolStripMenuItem.Size = new System.Drawing.Size(179, 30);
            this.solutionAToolStripMenuItem.Text = "Solution A";
            this.solutionAToolStripMenuItem.Click += new System.EventHandler(this.meanToolStripMenuItem_Click);
            // 
            // solutionBToolStripMenuItem
            // 
            this.solutionBToolStripMenuItem.Name = "solutionBToolStripMenuItem";
            this.solutionBToolStripMenuItem.Size = new System.Drawing.Size(179, 30);
            this.solutionBToolStripMenuItem.Text = "Solution B";
            this.solutionBToolStripMenuItem.Click += new System.EventHandler(this.solutionBToolStripMenuItem_Click);
            // 
            // solutionCToolStripMenuItem
            // 
            this.solutionCToolStripMenuItem.Name = "solutionCToolStripMenuItem";
            this.solutionCToolStripMenuItem.Size = new System.Drawing.Size(179, 30);
            this.solutionCToolStripMenuItem.Text = "Solution C";
            this.solutionCToolStripMenuItem.Click += new System.EventHandler(this.solutionCToolStripMenuItem_Click);
            // 
            // nonUniformMeanToolStripMenuItem
            // 
            this.nonUniformMeanToolStripMenuItem.Name = "nonUniformMeanToolStripMenuItem";
            this.nonUniformMeanToolStripMenuItem.Size = new System.Drawing.Size(297, 30);
            this.nonUniformMeanToolStripMenuItem.Text = "Non Uniform Matrix Filter";
            this.nonUniformMeanToolStripMenuItem.Click += new System.EventHandler(this.nonUniformMeanToolStripMenuItem_Click);
            // 
            // sobelToolStripMenuItem
            // 
            this.sobelToolStripMenuItem.Name = "sobelToolStripMenuItem";
            this.sobelToolStripMenuItem.Size = new System.Drawing.Size(297, 30);
            this.sobelToolStripMenuItem.Text = "Sobel 3x3";
            this.sobelToolStripMenuItem.Click += new System.EventHandler(this.sobelToolStripMenuItem_Click);
            // 
            // diferentiationToolStripMenuItem
            // 
            this.diferentiationToolStripMenuItem.Name = "diferentiationToolStripMenuItem";
            this.diferentiationToolStripMenuItem.Size = new System.Drawing.Size(297, 30);
            this.diferentiationToolStripMenuItem.Text = "Diferentiation";
            this.diferentiationToolStripMenuItem.Click += new System.EventHandler(this.diferentiationToolStripMenuItem_Click);
            // 
            // medianToolStripMenuItem
            // 
            this.medianToolStripMenuItem.Name = "medianToolStripMenuItem";
            this.medianToolStripMenuItem.Size = new System.Drawing.Size(297, 30);
            this.medianToolStripMenuItem.Text = "Median";
            this.medianToolStripMenuItem.Click += new System.EventHandler(this.medianToolStripMenuItem_Click);
            // 
            // robertsToolStripMenuItem
            // 
            this.robertsToolStripMenuItem.Name = "robertsToolStripMenuItem";
            this.robertsToolStripMenuItem.Size = new System.Drawing.Size(297, 30);
            this.robertsToolStripMenuItem.Text = "Roberts";
            this.robertsToolStripMenuItem.Click += new System.EventHandler(this.robertsToolStripMenuItem_Click);
            // 
            // autoZoomToolStripMenuItem
            // 
            this.autoZoomToolStripMenuItem.CheckOnClick = true;
            this.autoZoomToolStripMenuItem.Name = "autoZoomToolStripMenuItem";
            this.autoZoomToolStripMenuItem.Size = new System.Drawing.Size(285, 30);
            this.autoZoomToolStripMenuItem.Text = "Auto Zoom";
            this.autoZoomToolStripMenuItem.Click += new System.EventHandler(this.autoZoomToolStripMenuItem_Click);
            // 
            // histogramToolStripMenuItem
            // 
            this.histogramToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.grayToolStripMenuItem1,
            this.rGBToolStripMenuItem,
            this.grayRGBToolStripMenuItem});
            this.histogramToolStripMenuItem.Name = "histogramToolStripMenuItem";
            this.histogramToolStripMenuItem.Size = new System.Drawing.Size(285, 30);
            this.histogramToolStripMenuItem.Text = "Histogram";
            // 
            // grayToolStripMenuItem1
            // 
            this.grayToolStripMenuItem1.Name = "grayToolStripMenuItem1";
            this.grayToolStripMenuItem1.Size = new System.Drawing.Size(187, 30);
            this.grayToolStripMenuItem1.Text = "Gray";
            this.grayToolStripMenuItem1.Click += new System.EventHandler(this.histogramToolStripMenuItem_Click);
            // 
            // rGBToolStripMenuItem
            // 
            this.rGBToolStripMenuItem.Name = "rGBToolStripMenuItem";
            this.rGBToolStripMenuItem.Size = new System.Drawing.Size(187, 30);
            this.rGBToolStripMenuItem.Text = "RGB";
            this.rGBToolStripMenuItem.Click += new System.EventHandler(this.rGBToolStripMenuItem_Click);
            // 
            // grayRGBToolStripMenuItem
            // 
            this.grayRGBToolStripMenuItem.Name = "grayRGBToolStripMenuItem";
            this.grayRGBToolStripMenuItem.Size = new System.Drawing.Size(187, 30);
            this.grayRGBToolStripMenuItem.Text = "Gray + RGB";
            this.grayRGBToolStripMenuItem.Click += new System.EventHandler(this.grayRGBToolStripMenuItem_Click);
            // 
            // autoresToolStripMenuItem
            // 
            this.autoresToolStripMenuItem.Name = "autoresToolStripMenuItem";
            this.autoresToolStripMenuItem.Size = new System.Drawing.Size(77, 29);
            this.autoresToolStripMenuItem.Text = "Autors";
            this.autoresToolStripMenuItem.Click += new System.EventHandler(this.autoresToolStripMenuItem_Click);
            // 
            // evalToolStripMenuItem
            // 
            this.evalToolStripMenuItem.Name = "evalToolStripMenuItem";
            this.evalToolStripMenuItem.Size = new System.Drawing.Size(55, 29);
            this.evalToolStripMenuItem.Text = "Eval";
            this.evalToolStripMenuItem.Click += new System.EventHandler(this.EvalToolStripMenuItem_Click);
            // 
            // chessRecognitionToolStripMenuItem
            // 
            this.chessRecognitionToolStripMenuItem.Name = "chessRecognitionToolStripMenuItem";
            this.chessRecognitionToolStripMenuItem.Size = new System.Drawing.Size(169, 29);
            this.chessRecognitionToolStripMenuItem.Text = "Chess Recognition";
            this.chessRecognitionToolStripMenuItem.Click += new System.EventHandler(this.chessRecognitionToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.ImageViewer);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 35);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1114, 582);
            this.panel1.TabIndex = 6;
            // 
            // ImageViewer
            // 
            this.ImageViewer.Location = new System.Drawing.Point(0, 0);
            this.ImageViewer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ImageViewer.Name = "ImageViewer";
            this.ImageViewer.Size = new System.Drawing.Size(576, 427);
            this.ImageViewer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.ImageViewer.TabIndex = 6;
            this.ImageViewer.TabStop = false;
            this.ImageViewer.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ImageViewer_MouseClick_1);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1114, 617);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MainForm";
            this.Text = "Sistemas Sensoriais 2017/2018 - Image processing";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ImageViewer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void filtersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ImageViewer_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem imageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem colorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem negativeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem transformsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem translationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rotationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem filtersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem autoresToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem autoZoomToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox ImageViewer;
        private System.Windows.Forms.ToolStripMenuItem evalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem grayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem meanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nonUniformMeanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sobelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem diferentiationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem medianToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem histogramToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bWToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bWOtsuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem BrightnessContrasttoolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zoomToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mouseCenteredZoomToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redChannelGrayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nearestNeighbourToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem biLinearToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nearestNeighbourToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem biLinearToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem solutionAToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem solutionBToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem solutionCToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem robertsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem grayToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem rGBToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem grayRGBToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem chessRecognitionToolStripMenuItem;
    }
}

