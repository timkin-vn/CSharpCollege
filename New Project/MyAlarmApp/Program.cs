using System;
using System.Windows.Forms;

namespace MyAlarmApp
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1()); // Запускает твой будильник
        }
    }
}