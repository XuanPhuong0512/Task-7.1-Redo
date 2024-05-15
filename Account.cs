using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Validating_Accounts
{
    public class Account
    {
        private decimal _balance;
        public string AccountName {get;}

        public Account(string v, decimal initialBalance)
        {
            AccountName = v;
            _balance = initialBalance;
        }

        public decimal Balance
        {
            get { return _balance; }
        }

        public bool Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Deposit amount must be greater than zero.");
                return false;
            }

            _balance += amount;
            Console.WriteLine("Deposit successful. Current balance is: {0}", _balance);
            return true;
        }

        public bool Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Withdrawal amount must be greater than zero.");
                return false;
            }

            if (_balance < amount)
            {
                Console.WriteLine("Insufficient funds.");
                return false;
            }

            _balance -= amount;
            Console.WriteLine("Withdrawal successful. Current balance is: {0}", _balance);
            return true;
        }

        public void Print()
        {
            Console.WriteLine("Account balance: {0}", _balance);
        }
    }


} 
