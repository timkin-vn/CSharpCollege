using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.WPF.ViewModels
{
    public class AboutViewModel : ViewModelBase
    {
        public string Copyright
        {
            get
            {
                var attribute = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyCopyrightAttribute>();
                return attribute?.Copyright ?? "No Copyright Info";
            }
        }
        public string Version
        {
            get
            {
                var attribute = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyVersionAttribute>();
                return attribute?.Version ?? "1.0.0";
            }
        }
    }
}
