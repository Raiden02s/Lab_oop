using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApplication1
{
    public partial class Lab1 : Form
    {
        public Lab1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void calculate_Click(object sender, EventArgs e)
        {
            try
            {
                double x = Convert.ToDouble(InputX.Text);
                double y = (Math.Log10(Math.Cos( x ))/Math.Log10(1+x*x));
                Output.Text = y.ToString();
            }
            catch
            {
                
            }

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void Calculate_S_Click(object sender, EventArgs e)
        {
            try
            {
                double a = Convert.ToDouble(Input_a.Text);
                double b = Convert.ToDouble(Input_b.Text);
                double ang = Convert.ToDouble(Input_angle.Text);
                double S = (0.5*(a*b*(Math.Sin(ang*Math.PI/180))));
                Output_S.Text = S.ToString();
            }
            catch
            {

            }
        }

        private void Sim_chek_Click(object sender, EventArgs e)
        {
            try
            {
                double a = Convert.ToDouble(T1_side_a.Text);
                double b = Convert.ToDouble(T1_side_b.Text);

                double c = Convert.ToDouble(T2_side_c.Text);
                double d = Convert.ToDouble(T2_side_d.Text);

                double check_1 = (a / c);
                double check_2 = (b / d);
                if (check_1 == check_2)
                {
                    Is_similar.Text = "True";
                }
                else
                {
                    Is_similar.Text = "False";
                }

            }
            catch
            {

            }
        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void IsPositive_Click(object sender, EventArgs e)
        {
            double a = Convert.ToDouble(Number1.Text);
            double b = Convert.ToDouble(Number2.Text);
            double c = Convert.ToDouble(Number3.Text);

            if ((a + b) > 0 || (b + c) > 0 || (a + c) > 0)
            {
                Positive_Output.Text = "True";
            }
            else
            {
                Positive_Output.Text = "False";
            }
        }

        private void CalcAge_Click(object sender, EventArgs e)
        {

            int age = Convert.ToInt32(Age.Text);
            int birthDay = Convert.ToInt32(DayOfBirth.Text);
            int sumOfSquares = age.ToString().Select(c => (c - '0') * (c - '0')).Sum();
            int Calculated = sumOfSquares + birthDay;
            if (age > 100 && birthDay < 31)
            {
                CalculatedAge.Text = $"Calculated age: {Calculated}";
            }
            else
            {
                CalculatedAge.Text = "Wrong input";
            }
        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void CalcNewArrB_Click(object sender, EventArgs e)
        {
            try
            {
                int[] a = InputArrayA.Text.Split(',').Select(int.Parse).ToArray();
                int[] b = InputArrayB.Text.Split(',').Select(int.Parse).ToArray();

                if (a.Length != b.Length)
                {
                    MessageBox.Show("Sequences must have the same length");
                    return;
                }

                for (int i = 0; i < a.Length; i++)
                {
                    if (a[i] <= 0)
                        b[i] *= 10;
                    else
                        b[i] = 0;
                }

                NewArrayB.Text = "Transformed sequence: " + string.Join(", ", b);
            }
            catch (Exception)
            {
                MessageBox.Show("Invalid input. Please enter sequences as comma-separated integers.");
            }
        }

        private void CheckABC_Click(object sender, EventArgs e)
        {
            string inputText = SequenceInput.Text;
            int count = CountABCOccurrences(inputText);
            Check_abc_result.Text = $"Counter 'abc': {count}";

            MatchCollection matches = Regex.Matches(inputText, "abc");
        }


            public int CountABCOccurrences(string input)
        {
            MatchCollection matches = Regex.Matches(input, "abc");
            return matches.Count;

        }

        private void Output_Click(object sender, EventArgs e)
        {

        }
    }
}
