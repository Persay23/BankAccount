using BankAccountProject.BankAccount.States;
using BankAccountProject.Interfaces;

namespace BankAccountProject.BankAccount;

public class BankAccount(IClock clock)
{
    private decimal _balance = 0m;
    private DateTime _lastOperationTime = clock.UtcNow;
    internal readonly IClock _clock = clock;
    public event Action? OnReactivated;

    internal IAccountState State { get; set; } = new ActiveState();

    internal void UpdateLastOperationTime()
    {
        _lastOperationTime = _clock.UtcNow;
    }

    internal DateTime LastOperationTime => _lastOperationTime;

    public decimal Balance => _balance;
    internal void ChangeBalance(decimal amount)
    {
        _balance += amount;
    }

    internal bool IsVerified { get; set; } = false;

    public void Deposit(decimal amount)
    {
        State.Deposit(this, amount);
    }

    public void Withdraw(decimal amount)
    {
        State.Withdraw(this, amount);
    }

    public void Verify()
    {
        State.Verify(this);
    }

    public void Close()
    {
        State.Close(this);
    }

    public void CheckForDeactivation(TimeSpan inactivePeriod)
    {
        State.CheckForDeactivation(this, inactivePeriod);
    }

    internal void Reactivate()
    {
        OnReactivated?.Invoke();
    }
}