using System;
using System.Windows.Forms;

namespace FtpClientApp
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles(); // <= ці два рядки — обов'язкові
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
