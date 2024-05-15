
namespace Validating_Accounts
{
public abstract class Transaction {
    protected decimal _amount;
    protected bool _success;
    private bool _executed;    
    private bool _reversed;
    public DateTime _dateStamp;


    public Transaction(decimal amount) {
        _amount = amount;
        _executed = false;
        _success = false;
        _reversed = false;
    }

    public virtual void Execute()
        {
            if (_executed)
            {
                throw new InvalidOperationException("Transaction has already been executed.");
            }
            
            _executed = true;
            _success = true;
        }

    public virtual void Rollback()
        {
            if (!_executed)
            {
                throw new InvalidOperationException("Transaction has not been executed yet.");
            }

            if (_reversed)
            {
                throw new InvalidOperationException("Transaction has already been reversed.");
            }

            _reversed = true;
        }

    public abstract void Print();

    public bool Executed {
        get { return _executed; }
    }

    public bool Success {
        get { return _success; }
    }

    public bool Reversed {
        get { return _reversed; }
    }

    public DateTime DateStamp {
        get { return _dateStamp; }
    }
}

}
