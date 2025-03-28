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
            //Greeting();

            //Calculate();

            //Arrays();

            Arrays2d();

            Console.ReadKey();
        }

        private static void Greeting()
        {
            Console.Write("Представьтесь, пожалуйста: ");
            var name = Console.ReadLine();

            var greeting = $@"Привет, ""{name}""!";

            if (string.IsNullOrWhiteSpace(name))
            {
                greeting = @"Здравствуйте, ""незнакомец""!";
            }

            Console.WriteLine(greeting);
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

        private static void Arrays()
        {
            var array = new int[] { 1, 2, 3, 4, };

            //for (int i = 0; i < array.Length; i++)
            //{
            //    Console.Write($"{array[i]} ");
            //}

            //foreach (var item in array)
            //{
            //    Console.Write($"{item}, ");
            //}

            Console.Write(string.Join(", ", array));

            Console.WriteLine();
        }

        private static void Arrays2d()
        {
            var array2d = new[,]
            {
                { 1, 2, 3, 4, },
                { 5, 6, 7, 8, },
                { 9, 10, 11, 12, },
            };

            for (int row = 0; row < array2d.GetLength(0); row++)
            {
                for (int column = 0; column < array2d.GetLength(1); column++)
                {
                    Console.Write($"{array2d[row, column]} ");
                }

                Console.WriteLine();
            }

            Console.WriteLine();

            var arrayOfArray = new[]
            {
                new[] { 1, 2, 3, },
                new[] { 4, 5, 6, 7, },
                new[] { 8, 9, 10, 11, 12, },
            };

            for (int row = 0; row < arrayOfArray.Length; row++)
            {
                for (int column = 0; column < arrayOfArray[row].Length; column++)
                {
                    Console.Write($"{arrayOfArray[row][column]} ");
                }

                Console.WriteLine();
            }

            Console.WriteLine();

            foreach (var subArray in arrayOfArray)
            {
                foreach (var item in subArray)
                {
                    Console.Write($"{item}, ");
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }
    }
}
