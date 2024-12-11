using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Представьтесь, пожалуйста: ");
            string name = Console.ReadLine();
            string message = string.IsNullOrWhiteSpace(name) ? "Привет, незнакомец!" : $"Привет, {name}!";
            Console.WriteLine(message);
            Console.ReadKey();
        }
    }
}
