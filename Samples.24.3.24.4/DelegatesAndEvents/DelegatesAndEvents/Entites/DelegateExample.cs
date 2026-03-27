using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegatesAndEvents.Entites
{
    internal class DelegateExample
    {
        public void Run()
        {
            //SimpleMethod method = Hello;
            //method();

            //method = HowAreYou;
            //method();

            //method = () => Console.WriteLine("!!!");
            //method();

            RunGreeting(Hello);
            RunGreeting(HowAreYou);
            RunGreeting(() => Console.WriteLine("!!!"));

            Console.WriteLine($"Add: {RunOperation(Add, 4, 5)}");
            Console.WriteLine($"Multiply: {RunOperation(Multiply, 4, 5)}");

            Action greeting = Hello;
            greeting += HowAreYou;
            greeting += () => Console.WriteLine("Привет из лямбда-функции!");
            greeting?.Invoke();

            greeting -= Hello;
            greeting?.Invoke();

            greeting -= HowAreYou;
            greeting?.Invoke();

            Action<int, string> output = Output1;
            output += Output2;
            output += (x, s) => Console.WriteLine($"Вывод из лямбда-функции: \"{s}\" = {x}");
            output?.Invoke(5, "комментарий");

            Predicate<int> evenChecker = x => x % 2 == 0;
            Console.WriteLine($"Число 4 четно? {evenChecker(4)}");
            Console.WriteLine($"Число 5 четно? {evenChecker(5)}");
        }

        public delegate void SimpleMethod();

        public delegate int MathFunc(int x, int y);

        private void Hello()
        {
            Console.WriteLine("Привет!");
        }

        private void HowAreYou() => Console.WriteLine("Как дела?");

        //private void RunGreeting(SimpleMethod method)
        private void RunGreeting(Action method)
        {
            method();
        }

        private int Add(int x, int y)
        {
            return x + y;
        }

        private int Multiply(int x, int y) => x * y;

        //private int RunOperation(MathFunc operation, int z, int w)
        private int RunOperation(Func<int, int, int> operation, int z, int w)
        {
            return operation(z, w);
        }

        private void Output1(int x, string s) => Console.WriteLine($"{x}: {s}");

        private void Output2(int x, string s) => Console.WriteLine($"{s}: {x}");
    }
}
