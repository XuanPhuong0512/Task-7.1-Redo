using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validating_Accounts
{
    public class DepositTransaction : Transaction
{
    private readonly Account _account;
    private bool _executed;

    public DepositTransaction(Account account, decimal amount) : base(amount)
    {
        _account = account;
    }

    public override void Print()
    {
        if (_executed)
        {
            Console.WriteLine("Deposit transaction succeeded. Amount deposited: {0}", _amount);
        }
        else
        {
            Console.WriteLine("Deposit transaction has not been executed yet.");
        }
    }

    public override void Execute()
    {
        base.Execute();
        _account.Deposit(_amount);
        _executed = true;
    }

    public override void Rollback()
    {
        base.Rollback();

        if (!_success)
        {
            throw new InvalidOperationException("Deposit transaction was unsuccessful and cannot be reversed.");
        }

        _account.Withdraw(_amount);
    }
}
}
