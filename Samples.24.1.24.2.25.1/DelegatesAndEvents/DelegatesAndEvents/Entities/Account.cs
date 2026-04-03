using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegatesAndEvents.Entities
{
    internal class Account
    {
        public Account(decimal initialSum)
        {
            Sum = initialSum;
        }

        public decimal Sum { get; private set; }

        public event Action<string> Notify;

        public void Deposit(decimal amount)
        {
            Sum += amount;
            Notify?.Invoke($"На счет поступило: {amount}");
        }

        public void Withdraw(decimal amount)
        {
            if (Sum >= amount)
            {
                Sum -= amount;
                Notify?.Invoke($"Со счета снято: {amount}");
            }
            else
            {
                Notify?.Invoke($"Недостаточно средств на счете. Текущий баланс: {Sum}");
            }
        }
    }
}
