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
            //Greeting();

            //Calculate();

            //Arrays();

            Arrays2D();

            Console.ReadKey();
        }

        private static void Greeting()
        {
            Console.Write("Представьтесь, пожалуйста: ");
            var input = Console.ReadLine();

            var greetingMessage = $"Привет, {input}!";
            if (string.IsNullOrWhiteSpace(input))
            {
                greetingMessage = "Очень жаль, что вы так и не представились.";
            }

            Console.WriteLine(greetingMessage);
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

        private static void Arrays()
        {
            var newArray = new[] { 1, 2, 3, 4, 5 };

            //for (int i = 0; i < newArray.Length; i++)
            //{
            //    Console.Write($"{newArray[i]}, ");
            //}

            //Console.WriteLine();

            foreach (var value in newArray)
            {
                Console.Write($"{value}, ");
            }

            Console.WriteLine();

            Console.WriteLine(string.Join(", ", newArray));

            newArray = new int[4];

            Console.WriteLine(string.Join(", ", newArray));
        }

        private static void Arrays2D()
        {
            int[,] array2d = new int[,]
            {
                { 1, 2, 3, 4, },
                { 5, 6, 7, 8, },
                { 9, 10, 11, 12, },
                { 13, 14, 15, 16, },
            };

            for (int row = 0; row < array2d.GetLength(0); row++)
            {
                for (int col = 0; col < array2d.GetLength(1); col++)
                {
                    Console.Write($"{array2d[row, col]} ");
                }

                Console.WriteLine();
            }

            Console.WriteLine();

            int[][] arrayOfArrays = new int[][]
            {
                new[] { 1, 2, 3, },
                new[] { 4, 5, 6, 7, },
                new[] { 8, 9, 10, 11, 12, },
            };

            for (int row = 0; row < arrayOfArrays.Length; row++)
            {
                for (int col = 0; col < arrayOfArrays[row].Length; col++)
                {
                    Console.Write($"{arrayOfArrays[row][col]} ");
                }

                Console.WriteLine();
            }
        }
    }
}
