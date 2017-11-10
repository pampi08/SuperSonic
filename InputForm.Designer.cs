namespace SS_OpenCV
{
    partial class InputForm
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
            this.b2 = new System.Windows.Forms.NumericUpDown();
            this.b4 = new System.Windows.Forms.NumericUpDown();
            this.b5 = new System.Windows.Forms.NumericUpDown();
            this.b6 = new System.Windows.Forms.NumericUpDown();
            this.b7 = new System.Windows.Forms.NumericUpDown();
            this.b9 = new System.Windows.Forms.NumericUpDown();
            this.comboB = new System.Windows.Forms.ComboBox();
            this.b1 = new System.Windows.Forms.NumericUpDown();
            this.b3 = new System.Windows.Forms.NumericUpDown();
            this.b8 = new System.Windows.Forms.NumericUpDown();
            this.weightB = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.OKButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.b2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.b4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.b5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.b6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.b7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.b9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.b1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.b3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.b8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.weightB)).BeginInit();
            this.SuspendLayout();
            // 
            // b2
            // 
            this.b2.DecimalPlaces = 1;
            this.b2.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.b2.Location = new System.Drawing.Point(83, 41);
            this.b2.Name = "b2";
            this.b2.Size = new System.Drawing.Size(34, 20);
            this.b2.TabIndex = 3;
            this.b2.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.b2.ValueChanged += new System.EventHandler(this.b2_ValueChanged);
            // 
            // b4
            // 
            this.b4.DecimalPlaces = 1;
            this.b4.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.b4.Location = new System.Drawing.Point(43, 76);
            this.b4.Name = "b4";
            this.b4.Size = new System.Drawing.Size(34, 20);
            this.b4.TabIndex = 5;
            this.b4.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.b4.ValueChanged += new System.EventHandler(this.b4_ValueChanged);
            // 
            // b5
            // 
            this.b5.DecimalPlaces = 1;
            this.b5.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.b5.Location = new System.Drawing.Point(83, 76);
            this.b5.Name = "b5";
            this.b5.Size = new System.Drawing.Size(34, 20);
            this.b5.TabIndex = 6;
            this.b5.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.b5.ValueChanged += new System.EventHandler(this.b5_ValueChanged);
            // 
            // b6
            // 
            this.b6.DecimalPlaces = 1;
            this.b6.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.b6.Location = new System.Drawing.Point(123, 76);
            this.b6.Name = "b6";
            this.b6.Size = new System.Drawing.Size(34, 20);
            this.b6.TabIndex = 7;
            this.b6.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.b6.ValueChanged += new System.EventHandler(this.b6_ValueChanged);
            // 
            // b7
            // 
            this.b7.DecimalPlaces = 1;
            this.b7.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.b7.Location = new System.Drawing.Point(43, 112);
            this.b7.Name = "b7";
            this.b7.Size = new System.Drawing.Size(34, 20);
            this.b7.TabIndex = 8;
            this.b7.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.b7.ValueChanged += new System.EventHandler(this.b7_ValueChanged);
            // 
            // b9
            // 
            this.b9.DecimalPlaces = 1;
            this.b9.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.b9.Location = new System.Drawing.Point(123, 112);
            this.b9.Name = "b9";
            this.b9.Size = new System.Drawing.Size(34, 20);
            this.b9.TabIndex = 10;
            this.b9.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.b9.ValueChanged += new System.EventHandler(this.b9_ValueChanged);
            // 
            // comboB
            // 
            this.comboB.FormattingEnabled = true;
            this.comboB.Items.AddRange(new object[] {
            "Non-Uniform Mean 3x3",
            "Sobel 3x3"});
            this.comboB.Location = new System.Drawing.Point(39, 12);
            this.comboB.Name = "comboB";
            this.comboB.Size = new System.Drawing.Size(124, 21);
            this.comboB.TabIndex = 11;
            this.comboB.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // b1
            // 
            this.b1.DecimalPlaces = 1;
            this.b1.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.b1.Location = new System.Drawing.Point(43, 41);
            this.b1.Name = "b1";
            this.b1.Size = new System.Drawing.Size(34, 20);
            this.b1.TabIndex = 12;
            this.b1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.b1.ValueChanged += new System.EventHandler(this.b1_ValueChanged);
            // 
            // b3
            // 
            this.b3.DecimalPlaces = 1;
            this.b3.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.b3.Location = new System.Drawing.Point(123, 41);
            this.b3.Name = "b3";
            this.b3.Size = new System.Drawing.Size(34, 20);
            this.b3.TabIndex = 13;
            this.b3.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.b3.ValueChanged += new System.EventHandler(this.b3_ValueChanged);
            // 
            // b8
            // 
            this.b8.DecimalPlaces = 1;
            this.b8.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.b8.Location = new System.Drawing.Point(83, 112);
            this.b8.Name = "b8";
            this.b8.Size = new System.Drawing.Size(34, 20);
            this.b8.TabIndex = 14;
            this.b8.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.b8.ValueChanged += new System.EventHandler(this.b8_ValueChanged);
            // 
            // weightB
            // 
            this.weightB.DecimalPlaces = 1;
            this.weightB.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.weightB.Location = new System.Drawing.Point(106, 146);
            this.weightB.Name = "weightB";
            this.weightB.Size = new System.Drawing.Size(50, 20);
            this.weightB.TabIndex = 15;
            this.weightB.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.weightB.ValueChanged += new System.EventHandler(this.weightB_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(41, 150);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "WEIGHT:";
            // 
            // OKButton
            // 
            this.OKButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OKButton.Location = new System.Drawing.Point(39, 181);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(124, 23);
            this.OKButton.TabIndex = 17;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButton.Location = new System.Drawing.Point(39, 210);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(124, 23);
            this.CancelButton.TabIndex = 19;
            this.CancelButton.Text = "CANCEL";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // InputForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(208, 257);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.weightB);
            this.Controls.Add(this.b8);
            this.Controls.Add(this.b3);
            this.Controls.Add(this.b1);
            this.Controls.Add(this.comboB);
            this.Controls.Add(this.b9);
            this.Controls.Add(this.b7);
            this.Controls.Add(this.b6);
            this.Controls.Add(this.b5);
            this.Controls.Add(this.b4);
            this.Controls.Add(this.b2);
            this.Name = "InputForm";
            this.Text = "                                                                 ";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.b2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.b4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.b5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.b6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.b7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.b9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.b1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.b3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.b8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.weightB)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox comboB;
        public System.Windows.Forms.NumericUpDown b1;
        public System.Windows.Forms.NumericUpDown b2;
        public System.Windows.Forms.NumericUpDown b4;
        public System.Windows.Forms.NumericUpDown b5;
        public System.Windows.Forms.NumericUpDown b6;
        public System.Windows.Forms.NumericUpDown b7;
        public System.Windows.Forms.NumericUpDown b9;
        public System.Windows.Forms.NumericUpDown b3;
        public System.Windows.Forms.NumericUpDown b8;
        public System.Windows.Forms.NumericUpDown weightB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button OKButton;
        private new System.Windows.Forms.Button CancelButton;
    }
}