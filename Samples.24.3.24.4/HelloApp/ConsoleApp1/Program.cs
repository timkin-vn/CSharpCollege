using ConsoleApp1.Classes;
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
            //Console.Write("Представьтесь, пожалуйста: ");
            //string name = Console.ReadLine();

            //Console.Write("Введите ваш возраст: ");
            //var input = Console.ReadLine();

            //if (!int.TryParse(input, out var age))
            //{
            //    Console.WriteLine("Вы не указали свой возраст.");
            //    return;
            //}

            //if (string.IsNullOrWhiteSpace(name))
            //{
            //    Console.WriteLine("Привет, незнакомец!");
            //}
            //else
            //{
            //    Console.WriteLine($"Привет, {name}! Ваш возраст {age} лет.");
            //}

            var tc1 = new TestClass { Value = 5 };
            var tc2 = new TestClass { Value = 7 };
            Console.WriteLine($"tc1.Value = {tc1.Value}, tc2.Value = {tc2.Value}");

            tc2 = tc1;
            Console.WriteLine($"tc1.Value = {tc1.Value}, tc2.Value = {tc2.Value}");

            tc1.Value = 10;
            Console.WriteLine($"tc1.Value = {tc1.Value}, tc2.Value = {tc2.Value}");

            var ts1 = new TestStruct { Value = 5 };
            var ts2 = new TestStruct { Value = 7 };
            Console.WriteLine($"ts1.Value = {ts1.Value}, ts2.Value = {ts2.Value}");

            ts2 = ts1;
            Console.WriteLine($"ts1.Value = {ts1.Value}, ts2.Value = {ts2.Value}");

            ts1.Value = 10;
            Console.WriteLine($"ts1.Value = {ts1.Value}, ts2.Value = {ts2.Value}");
        }
    }
}
