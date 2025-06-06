using System;
using System.Drawing;
using System.Windows.Forms;

namespace ParametricCurvePlotter
{
    public partial class Form1 : Form
    {
        private float a = 1.0f; // Default value for coefficient a

        public Form1()
        {
            InitializeComponent();
        }

        private void buttonPlot_Click(object sender, EventArgs e)
        {
            if (float.TryParse(textBoxA.Text, out float value))
            {
                a = value;
                pictureBox1.Refresh();
            }
            else
            {
                MessageBox.Show("Please enter a valid number for 'a'.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            int width = pictureBox1.Width;
            int height = pictureBox1.Height;
            float scale = 50.0f; // Scaling factor for graph
            float centerX = width / 2.0f;
            float centerY = height / 2.0f;

            // Draw axes
            g.DrawLine(Pens.Black, 0, (int)centerY, width, (int)centerY); // X-axis
            g.DrawLine(Pens.Black, (int)centerX, 0, (int)centerX, height); // Y-axis

            // Label axes
            g.DrawString("X", new Font("Arial", 10), Brushes.Black, width - 15, centerY - 5);
            g.DrawString("Y", new Font("Arial", 10), Brushes.Black, centerX + 5, 5);

            // Add tick marks and labels (e.g., -5 to 5)
            for (int i = -5; i <= 5; i++)
            {
                float x = centerX + i * scale;
                float y = centerY - i * scale;
                g.DrawLine(Pens.Black, x, centerY - 2, x, centerY + 2); // X-axis ticks
                g.DrawLine(Pens.Black, centerX - 2, y, centerX + 2, y); // Y-axis ticks
                g.DrawString(i.ToString(), new Font("Arial", 8), Brushes.Black, x - 5, centerY + 5);
                g.DrawString(i.ToString(), new Font("Arial", 8), Brushes.Black, centerX + 5, y - 5);
            }

            // Plot parametric curve
            PointF[] points = new PointF[1000];
            int pointIndex = 0;

            for (float t = -5.0f; t <= 5.0f; t += 0.01f)
            {
                if (Math.Abs(1 + t * t * t) < 0.001f) continue; // Avoid division by zero near t = -1
                float x = (3 * a * t) / (1 + t * t * t);
                float y = (3 * a * t * t) / (1 + t * t * t);
                points[pointIndex] = new PointF(centerX + x * scale, centerY - y * scale);
                pointIndex++;
            }

            // Create a new array with the exact number of points to draw
            PointF[] pointsToDraw = new PointF[pointIndex];
            Array.Copy(points, 0, pointsToDraw, 0, pointIndex);

            // Draw the curve
            if (pointsToDraw.Length > 1) // Ensure there are at least 2 points to draw a line
            {
                g.DrawLines(Pens.Red, pointsToDraw);
            }
        }

        private void textBoxA_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}