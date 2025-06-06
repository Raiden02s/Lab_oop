using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Win3Thread
{
    public partial class Form1 : Form
    {
        private Thread thread1;
        private Thread thread2;
        private Thread thread3;

        private bool stop1 = false;
        private bool stop2 = false;
        private bool stop3 = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void draw_rect()
        {
            Random rnd = new Random();
            Graphics g = panel1.CreateGraphics();
            while (!stop1)
            {
                Thread.Sleep(100);
                int w = rnd.Next(20, panel1.Width);
                int h = rnd.Next(20, panel1.Height);
                g.DrawRectangle(Pens.Blue, 0, 0, w, h);
            }
        }

        private void draw_eclipse()
        {
            Random rnd = new Random();
            Graphics g = panel2.CreateGraphics();
            while (!stop2)
            {
                Thread.Sleep(100);
                int w = rnd.Next(20, panel2.Width);
                int h = rnd.Next(20, panel2.Height);
                g.DrawEllipse(Pens.Red, 0, 0, w, h);
            }
        }

        private void Rnd_num()
        {
            Random rnd = new Random();
            for (int i = 0; i < 1000 && !stop3; i++)
            {
                Thread.Sleep(10);
                richTextBox1.Invoke((MethodInvoker)(() =>
                {
                    richTextBox1.AppendText(((char)rnd.Next(33, 126)).ToString());
                }));
            }
        }

        private void buttonStartAll_Click(object sender, EventArgs e)
        {
            stop1 = stop2 = stop3 = false;
            thread1 = new Thread(draw_rect);
            thread2 = new Thread(draw_eclipse);
            thread3 = new Thread(Rnd_num);
            thread1.Start();
            thread2.Start();
            thread3.Start();
        }

        private void buttonStopAll_Click(object sender, EventArgs e)
        {
            stop1 = stop2 = stop3 = true;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            stop1 = stop2 = stop3 = true;
            thread1?.Join();
            thread2?.Join();
            thread3?.Join();
        }
    }
}
