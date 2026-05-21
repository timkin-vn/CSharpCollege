using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using LimerList.Models;

namespace LimerList.ViewModels
{
    public sealed class AboutViewModel : ViewModelBase
    {
        public string Copyright
        {
            get
            {
                var copyright = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyCopyrightAttribute>();
                return copyright?.Copyright ?? "No copyright info";
            }
            
        }
        public string Version
        {
            get
            {
                var assembly = Assembly.GetExecutingAssembly().GetName();
                var version = assembly.Version;
                return version?.ToString() ?? "1.0";
            }
        }
        public string FileVersion
        {
            get
            {
                var fileVersion = System.Diagnostics.FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion;
                return fileVersion ?? "1.0";
            }
        }
        public string Seminar
        {
            get
            {
                var assembly = Assembly.GetExecutingAssembly();
                var seminar = assembly.GetCustomAttribute<AssemblySeminarAttribute>();
                return seminar?.Title;
            }
        }
        public string Monitor
        {
            get
            {
                var assembly = Assembly.GetExecutingAssembly();
                var monitor = assembly.GetCustomAttribute<AssemblyMonitorAttribute>();
                return monitor?.Name;
            }
        }
    }
}
