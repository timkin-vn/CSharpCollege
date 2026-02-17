using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var now = DateTime.Now;
            Console.WriteLine(now.ToString());
            Console.WriteLine(now.ToShortDateString());
            Console.WriteLine(now.ToLongDateString());
            Console.WriteLine(now.ToShortTimeString());
            Console.WriteLine(now.ToLongTimeString());

            var thenTime = now.AddHours(3);
            Console.WriteLine($"Через 3 часа будет {thenTime.ToLongTimeString()}");

            var timeDiff = thenTime - now;
            Console.WriteLine($"Разница во времени {timeDiff}");

            var birthDate = new DateTime(1988, 11, 15);
            var lived = now - birthDate;
            Console.WriteLine($"Человек прожил {lived.TotalDays} дней");
        }
    }
}
