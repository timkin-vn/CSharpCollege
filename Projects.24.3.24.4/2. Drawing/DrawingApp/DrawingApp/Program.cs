using System;
using System.Windows.Forms;

namespace DrawingApp
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1()); // Имя вашей формы
        }
    }
}