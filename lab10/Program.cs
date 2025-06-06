using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

// Додай using з правильним namespace з Form1.cs (заміни, якщо у тебе інший)
using lab10;
using Win3Thread;

namespace lab10
{
    internal static class Program
    {
        /// <summary>
        /// Головна точка входу для застосунку.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1()); // Запускає головну форму
        }
    }
}
