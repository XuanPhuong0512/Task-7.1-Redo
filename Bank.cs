using System;
using System.Collections.Generic;

namespace Validating_Accounts 
{
public class Bank {
    private List<Account> _accounts;
    private List<Transaction> _transactions;
    public Bank() {
        _accounts = new List<Account>();
        _transactions = new List<Transaction>();
    }

    public void AddAccount(Account account) {
        _accounts.Add(account);
    }

    public Account GetAccount(string name) {
        foreach (Account account in _accounts) {
            if (account.AccountName == name) {
                return account;
            }
        }
        return null;
    }

    public void ExecuteTransaction(Transaction transaction)
    {
        transaction._dateStamp = DateTime.Now;
        _transactions.Add(transaction);
        transaction.Execute();
    }

        public void RollbackTransaction(int index)
        {
            // Adjust for 1-based index if necessary
            int adjustedIndex = index - 1;

            // Check if the adjusted index is within the bounds of the _transactions list
            if (adjustedIndex >= 0 && adjustedIndex < _transactions.Count)
            {
                _transactions[adjustedIndex].Rollback();
            }
            else
            {
                // If the adjusted index is out of bounds, print an error message or handle it as needed
                Console.WriteLine("Error: Transaction index is out of range.");
            }
        }

        public void PrintTransactionHistory() 
    {
        foreach (Transaction transaction in _transactions) 
        {
            Console.Write("Transaction status: ");
            if (transaction.Reversed) 
            {
                Console.WriteLine("Reversed at " + transaction.DateStamp);
            } 
            else if (transaction.Success) 
            {
                Console.WriteLine("Executed at " + transaction.DateStamp);
            } 
            else 
            {
                Console.WriteLine("Failed at " + transaction.DateStamp);
            }
            transaction.Print();
        }
    }
}

}
