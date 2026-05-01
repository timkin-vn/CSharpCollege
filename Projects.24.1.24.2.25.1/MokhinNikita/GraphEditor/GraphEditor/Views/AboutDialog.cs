using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphEditor.Views
{
    public partial class AboutDialog : Form
    {
        private string Copyright
        {
            get
            {
                var asssembly = Assembly.GetExecutingAssembly();
                var attribute = asssembly.GetCustomAttribute<AssemblyCopyrightAttribute>();
                return attribute?.Copyright ?? "";
            }
        }
        private string Version
        {
            get
            {
                var asssemblyName = Assembly.GetExecutingAssembly().GetName();
                var version = asssemblyName.Version;
                return version?.ToString() ?? "";
            }
        }
        public AboutDialog()
        {
            InitializeComponent();
            string[] informations =
            {
                Copyright,
                Version,
            };
            label1.Text = string.Join("\n", from information in informations where !string.IsNullOrEmpty(information) select information);
        }
    }
}
