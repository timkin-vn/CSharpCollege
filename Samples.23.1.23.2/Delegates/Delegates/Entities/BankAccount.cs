using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegates.Entities
{
    internal class BankAccount
    {
        public BankAccount(decimal initialSum)
        {
            Sum = initialSum;
        }

        public decimal Sum { get; set; }

        public event Action<string> Notify;

        public void Deposit(decimal amount)
        {
            Sum += amount;
            Notify?.Invoke($"На счет поступило: {amount}");
        }

        public bool Withdraw(decimal amount)
        {
            if (Sum >= amount)
            {
                Sum -= amount;
                Notify?.Invoke($"Со счета снято: {amount}");
                return true;
            }

            Notify?.Invoke($"Недостаточно средств на счете. Текущий баланс: {Sum}");
            return false;
        }
    }
}
