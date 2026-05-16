using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LimerList.Models
{
    [AttributeUsage(AttributeTargets.Assembly, Inherited = false), ComVisible(true)]
    public class AssemblyMonitorAttribute : Attribute
    {
        private readonly string _name;

        public AssemblyMonitorAttribute(string name)
        {
            _name = name;
        }
        public string Name => !string.IsNullOrWhiteSpace(_name) ? $"Мониторинг: {_name}" : "Unknown Monitor";
    }
}
