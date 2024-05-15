
namespace Validating_Accounts
{
public class TransferTransaction : Transaction {
    private Account _fromAccount;
    private Account _toAccount;
    private DepositTransaction _deposit;
    private WithdrawTransaction _withdraw;
        
    
    public TransferTransaction(Account fromAccount, Account toAccount, decimal amount) : base(amount)
    {
        _fromAccount = fromAccount;
        _toAccount = toAccount;
        
        _withdraw = new WithdrawTransaction(_fromAccount, _amount);
        _deposit = new DepositTransaction(_toAccount, _amount);
    }
    
    public override void Print() {
        Console.WriteLine("Transferred $" + _amount + " from " + _fromAccount.AccountName + "'s account to " + _toAccount.AccountName + "'s account.");
        _deposit.Print();
        _withdraw.Print();
    }
    
    public override void Execute()
    {
        base.Execute();
        _withdraw.Execute();
        _deposit.Execute();
    }
    
    public override void Rollback()
        {
            if (!Success)
            {
                throw new InvalidOperationException("This transfer transaction was not successfully completed.");
            }
            if (Reversed)
            {
                throw new InvalidOperationException("This transfer transaction has already been reversed.");
            }

            _deposit.Rollback();
            _withdraw.Rollback();
            base.Rollback();
        }

    public bool Success
    {
        get { return _deposit.Success && _withdraw.Success && base.Success; }
    }

    public bool Reversed
    {
        get { return _deposit.Reversed && _withdraw.Reversed && base.Reversed; }
    }

    public bool Executed
    {
        get { return _deposit.Executed && _withdraw.Executed && base.Executed; }
    }
}

}
