using HelloApp.Classes;
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
            //string input = Console.ReadLine();

            //Console.Write("Введите ваш возраст: ");
            //var ageInput = Console.ReadLine();
            //if (!int.TryParse(ageInput, out var age))
            //{
            //    Console.WriteLine("Вы скрываете свой возраст? До свидания.");
            //    return;
            //}

            ////if (input == "")
            ////if (input == string.Empty)
            //if (string.IsNullOrWhiteSpace(input))
            //{
            //    Console.WriteLine($"Здравствуйте, незнакомец! Вам {age} лет.");
            //}
            //else
            //{
            //    Console.WriteLine($"Привет, {input}! Вам {age} лет.");
            //}

            var tc1 = new TestClass { Value = 5 };
            var tc2 = new TestClass { Value = 7 };

            Console.WriteLine($"Initial values: tc1.Value = {tc1.Value}, tc2.Value = {tc2.Value}");

            tc2 = tc1;

            Console.WriteLine($"After assignment: tc1.Value = {tc1.Value}, tc2.Value = {tc2.Value}");

            tc1.Value = 10;

            Console.WriteLine($"After value update: tc1.Value = {tc1.Value}, tc2.Value = {tc2.Value}");

            var ts1 = new TestStruct { Value = 5 };
            var ts2 = new TestStruct { Value = 7 };

            Console.WriteLine($"Initial values: ts1.Value = {ts1.Value}, ts2.Value = {ts2.Value}");

            ts2 = ts1;

            Console.WriteLine($"After assignment: ts1.Value = {ts1.Value}, ts2.Value = {ts2.Value}");

            ts1.Value = 10;

            Console.WriteLine($"After value update: ts1.Value = {ts1.Value}, ts2.Value = {ts2.Value}");
        }
    }
}
