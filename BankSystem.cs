using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validating_Accounts
{
    using System;

    namespace BankSystem
    {
        public enum MenuOption
        {
            Add = 1,
            Withdraw = 2,
            Deposit = 3,
            Transfer = 4,
            Print = 5,
            PrintTransactionHistory = 6,
            Quit = 7
        }

        class BankSystem
        {
            static void Main(string[] args)
            {
                Bank bank = new Bank();
                do
                {
                    MenuOption option = ReadUserOption();

                    switch (option)
                    {
                        case MenuOption.Add:
                            Console.Write("Enter the name of the account: ");
                            string name = Console.ReadLine();
                            Console.Write("Enter the starting balance: ");
                            decimal amount = decimal.Parse(Console.ReadLine());
                            Account a = new Account(name, amount);
                            bank.AddAccount(a);
                            // account added successfully
                            // Console.WriteLine(withdrawResult ? "Withdrawal successful" : "Withdrawal failed");
                            break;

                        case MenuOption.Withdraw:
                            Console.Write("Enter amount to withdraw: ");
                            decimal withdrawAmount = decimal.Parse(Console.ReadLine());
                            bool withdrawResult = DoWithdraw(bank, withdrawAmount);
                            Console.WriteLine(withdrawResult ? "Withdrawal successful" : "Withdrawal failed");
                            break;

                        case MenuOption.Deposit:
                            Console.Write("Enter amount to deposit: ");
                            decimal depositAmount = decimal.Parse(Console.ReadLine());
                            bool depositResult = DoDeposit(bank, depositAmount);
                            Console.WriteLine(depositResult ? "Deposit successful" : "Deposit failed");
                            break;

                        case MenuOption.Transfer:
                            Console.Write("Enter amount to transfer: ");
                            decimal transferAmount = decimal.Parse(Console.ReadLine());
                            bool transferResult = DoTransfer(bank, transferAmount);
                            Console.WriteLine(transferResult ? "Transfer successful" : "Transfer failed");
                            break;

                        case MenuOption.Print:
                            DoPrint(bank);
                            break;

                        case MenuOption.PrintTransactionHistory:
                            DoPrintTransactionHistory(bank);
                            break;

                        case MenuOption.Quit:
                            Console.WriteLine("Goodbye!");
                            return;
                    }
                } while (true);
            }

            static MenuOption ReadUserOption()
            {
                int option;
                bool validOption = false;

                do
                {
                    Console.WriteLine("Select an option:");
                    Console.WriteLine("1. Add new account");
                    Console.WriteLine("2. Withdraw");
                    Console.WriteLine("3. Deposit");
                    Console.WriteLine("4. Transfer");
                    Console.WriteLine("5. Print");
                    Console.WriteLine("6. Print transaction history");
                    Console.WriteLine("7. Quit");
                    Console.Write("Enter option number: ");

                    if (int.TryParse(Console.ReadLine(), out option))
                    {
                        if (option >= 1 && option <= 7)
                        {
                            validOption = true;
                        }
                    }

                    if (!validOption)
                    {
                        Console.WriteLine("Invalid option. Try again.");
                    }
                } while (!validOption);

                return (MenuOption)option;
            }

            static bool DoDeposit(Bank bank, decimal amount)
            {
                Account account = FindAccount(bank);
                if (amount > 0 && account != null)
                {
                    DepositTransaction dt = new DepositTransaction(account, amount);
                    bank.ExecuteTransaction(dt);
                    return true;
                }
                else
                {
                    return false;
                }
            }

            static bool DoWithdraw(Bank bank, decimal amount)
            {
                Account account = FindAccount(bank);
                if (amount > 0 && account.Balance >= amount)
                {
                    WithdrawTransaction wt = new WithdrawTransaction(account, amount);
                    bank.ExecuteTransaction(wt);
                    return true;
                }
                else
                {
                    return false;
                }
            }

            static bool DoTransfer(Bank bank, decimal amount)
            {      
                Account source = FindAccount(bank);
                Account destination = FindAccount(bank);

                if (source != null && destination != null)
                {
                    TransferTransaction tt = new TransferTransaction(source, destination, amount);
                    bank.ExecuteTransaction(tt);
                    return true;
                }
                else
                {
                    return false;
                }
            }


            static void DoPrint(Bank bank)
            {
                Account account = FindAccount(bank);
                if (account != null)
                {
                    account.Print();
                }
            }


            private static Account FindAccount(Bank bank)
            {
                Console.Write("Enter the name of the account: ");
                string name = Console.ReadLine();
                Account account = bank.GetAccount(name);
                if (account == null)
                {
                    Console.WriteLine("Account not found.");
                }
                return account;
            }

            private static void DoPrintTransactionHistory(Bank bank)
            {
                Console.WriteLine("Transaction History: ");
                bank.PrintTransactionHistory();
                Console.WriteLine("Do you want to rollback a specific transaction? [y/n] ");
                if(Console.ReadLine() == "y") 
                {
                    DoRollback(bank);
                }
            }

            public static void DoRollback(Bank bank)
            {
                try
                {
                    Console.WriteLine("Which transaction would you like to rollback? Enter its index:");
                    string input = Console.ReadLine();
                    if (!string.IsNullOrEmpty(input))
                    {
                        int index = int.Parse(input);
                        bank.RollbackTransaction(index);
                        Console.WriteLine("Transaction rolled back successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Transaction rollback failed.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }


        }

    }
}
