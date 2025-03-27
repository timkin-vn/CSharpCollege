using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hello
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Представьтесь, пожалуйста: ");
            var name = Console.ReadLine();

            var greeting = $@"Привет, ""{name}""!";

            if (string.IsNullOrWhiteSpace(name))
            {
                greeting = @"Здравствуйте, ""незнакомец""!";
            }

            Console.WriteLine(greeting);

            Calculate();

            Console.ReadKey();
        }

        private static void Calculate()
        {
            Console.Write("Введите первое слагаемое: ");
            var input = Console.ReadLine();
            if (!int.TryParse(input, out var x))
            {
                Console.WriteLine("Внимательнее!");
                return;
            }

            Console.Write("Введите второе слагаемое: ");
            input = Console.ReadLine();
            var y = int.Parse(input);

            var sum = x + y;

            Console.WriteLine($"Сумма = {sum}");
        }
    }
}
