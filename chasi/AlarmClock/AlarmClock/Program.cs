using System;
using System.Drawing;
using System.Windows.Forms;

namespace AlarmClock
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            while (true)
            {
                string userAnswer = ShowSimpleInputDialog("2 + 2?");
                if (userAnswer.Trim() == "4")
                {
                    break;
                }
                else
                {
                    MessageBox.Show("Risposta errata. Riprova.", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            Application.Run(new ClockForm());
        }

        private static string ShowSimpleInputDialog(string question)
        {
            Form inputForm = new Form()
            {
                Width = 300,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "Verifica",
                StartPosition = FormStartPosition.CenterScreen,
                MaximizeBox = false,
                MinimizeBox = false
            };

            Label lbl = new Label() { Left = 20, Top = 20, Text = question, AutoSize = true };
            TextBox txt = new TextBox() { Left = 20, Top = 50, Width = 240 };
            Button btnOk = new Button() { Text = "OK", Left = 190, Width = 70, Top = 80, DialogResult = DialogResult.OK };

            inputForm.Controls.Add(lbl);
            inputForm.Controls.Add(txt);
            inputForm.Controls.Add(btnOk);
            inputForm.AcceptButton = btnOk;

            return inputForm.ShowDialog() == DialogResult.OK ? txt.Text : "";
        }
    }
}
