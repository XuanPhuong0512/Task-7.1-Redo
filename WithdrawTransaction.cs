using System;


namespace Validating_Accounts
{
    public class WithdrawTransaction : Transaction
    {
        private Account _account;
        private bool _executed;

        public WithdrawTransaction(Account account, decimal amount) : base(amount)
        {
            _account = account;
            _executed = false;
        }

        public override void Print()
        {
            if (_executed)
            {
                if (_success)
                {
                    Console.WriteLine("Withdraw transaction succeeded. Amount withdrawn: {0}", _amount);
                }
                else
                {
                    Console.WriteLine("Withdraw transaction failed. Insufficient funds.");
                }
            }
            else
            {
                Console.WriteLine("Withdraw transaction has not been executed yet.");
            }
        }

        public override void Execute()
        {
            base.Execute();

            if (_account.Balance < _amount)
            {
                throw new InvalidOperationException("Insufficient funds.");
            }

            _account.Withdraw(_amount);
            _success = true;
            _executed = true;
        }

        public override void Rollback()
        {
            base.Rollback();

            if (!_success)
            {
                throw new InvalidOperationException("Withdraw transaction was unsuccessful and cannot be reversed.");
            }

            _account.Deposit(_amount);
        }
    }


}
