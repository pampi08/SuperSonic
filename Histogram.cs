using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SS_OpenCV
{
    public partial class Histogram : Form
    {
        private int[] histogram = null;

        public Histogram(string _title, int[] histogram)
        {
            InitializeComponent();
            this.Text = _title;
            this.histogram = histogram;
        }

        private void Histogram_Load(object sender, EventArgs e)
        {
            
            // Bind the chart to the list. 
            chart1.Series[0].Points.DataBindY(this.histogram);
           
            chart1.Series[0].Color = Color.Gray;
            chart1.ChartAreas[0].AxisX.Maximum = 255;
            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisX.Title = "Intensidade";
            chart1.ChartAreas[0].AxisY.Title = "Numero Pixeis";
            chart1.ResumeLayout();

        }

        

        private void chart1_Click(object sender, EventArgs e)
        {

        }
    }
}
