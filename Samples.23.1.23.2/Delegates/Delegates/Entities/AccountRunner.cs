using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegates.Entities
{
    internal class AccountRunner
    {
        public void Run()
        {
            var account = new BankAccount(100);
            account.Notify += DisplayMessage;
            account.Notify += DisplayRedMessage;
            Console.WriteLine($"Сумма на счете: {account.Sum}");

            account.Deposit(20);
            Console.WriteLine($"Сумма на счете: {account.Sum}");

            account.Withdraw(70);
            Console.WriteLine($"Сумма на счете: {account.Sum}");

            account.Withdraw(100);
            Console.WriteLine($"Сумма на счете: {account.Sum}");
        }

        private void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }

        private void DisplayRedMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
