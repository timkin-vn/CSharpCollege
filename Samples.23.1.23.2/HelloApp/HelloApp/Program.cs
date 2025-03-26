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
            var input = Console.ReadLine();

            var greetingMessage = $"Привет, {input}!";
            if (string.IsNullOrWhiteSpace(input))
            {
                greetingMessage = "Очень жаль, что вы так и не представились.";
            }

            Console.WriteLine(greetingMessage);

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

            Console.WriteLine($"Сумма равна {sum}");
        }
    }
}
