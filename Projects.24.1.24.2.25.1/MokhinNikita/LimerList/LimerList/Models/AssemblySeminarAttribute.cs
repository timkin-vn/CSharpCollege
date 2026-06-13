using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LimerList.Models
{
    [AttributeUsage(AttributeTargets.Assembly, Inherited = false), ComVisible(true)]
    public class AssemblySeminarAttribute : Attribute
    {
        private readonly string _title;
        public AssemblySeminarAttribute(string title)
        {
            _title = title;
        }
        public string Title => !string.IsNullOrWhiteSpace(_title) ? $"Семинар: {_title}" : "Unknown seminar";
    }
}
