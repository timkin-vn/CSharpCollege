using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegates.Entities
{
    internal class DelegatesExample
    {
        public void Run()
        {
            RunGreeting(Hello);
            RunGreeting(HowAreYou);

            Console.WriteLine($"Add: {RunOperation(Add, 4, 5)}");
            Console.WriteLine($"Multiply: {RunOperation(Multiply, 4, 5)}");

            Action greeting = Hello;
            greeting += HowAreYou;
            greeting?.Invoke();

            greeting -= Hello;
            greeting?.Invoke();

            greeting -= HowAreYou;
            greeting?.Invoke();

            Action<int, string> output = Output1;
            output += Output2;
            output?.Invoke(5, "пояснение");

            Predicate<int> checker = IsEven;
            Console.WriteLine($"{checker(5)}");
        }

        public delegate void GreetingMethod();

        public delegate int Operation(int x, int y);

        private void RunGreeting(Action greeting)
        {
            greeting();
        }

        private int RunOperation(Func<int, int, int> operation, int x, int y)
        {
            return operation(x, y);
        }

        private void Hello()
        {
            Console.WriteLine("Привет!");
        }

        private void HowAreYou()
        {
            Console.WriteLine("Как дела?");
        }

        private void HelloNamed(string userName)
        {
            Console.WriteLine($"Привет, {userName}!");
        }

        private int Add(int i, int j)
        {
            return i + j;
        }

        private int Multiply(int m, int n)
        {
            return m * n;
        }

        private void Output1(int x, string s) => Console.WriteLine($"{x}: {s}");

        private void Output2(int x, string s) => Console.WriteLine($"{s}: {x}");

        bool IsEven(int x) => x % 2 == 0;
    }
}
