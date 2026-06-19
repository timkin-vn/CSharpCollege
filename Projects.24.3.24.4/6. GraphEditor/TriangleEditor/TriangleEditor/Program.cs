using System;
using System.Windows.Forms;

namespace TriangleEditor
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new TriangleEditorForm());
        }
    }
}
