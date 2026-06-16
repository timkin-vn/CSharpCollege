using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hello.Types
{
    internal struct PersonStruct
    {
        private string _name;

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public int Age { get; private set; }

        public string PrintString => $"Имя {Name}, возраст {Age}.";

        public int BirthYear
        {
            set
            {
                Age = 2025 - value;
            }
        }

        public void PrintOut()
        {
            Console.WriteLine($"Имя {Name}, возраст {Age}.");
        }
    }
}
