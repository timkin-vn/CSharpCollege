using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var now = DateTime.Now;
            Console.WriteLine($"Сейчас {now.ToString()}");
            Console.WriteLine($"Сейчас {now.ToLongDateString()}");
            Console.WriteLine($"Сейчас {now.ToShortDateString()}");
            Console.WriteLine($"Сейчас {now.ToLongTimeString()}");
            Console.WriteLine($"Сейчас {now.ToShortTimeString()}");
            Console.WriteLine($"Сейчас {now.ToString("yyyy-MM-d")}");

            var there = now.AddHours(3);
            Console.WriteLine($"Сейчас {there.ToLongTimeString()}");

            var timeDiff = there - now;
            Console.WriteLine($"Интервал равен {timeDiff}");

            var baseDate = new DateTime(1974, 4, 12, 11, 9, 25);
            var lived = now - baseDate;
            Console.WriteLine($"Прожито {lived.TotalDays} дней");
        }
    }
}
