namespace BankAccountProject.BankAccount.States;

public class ClosedState : IAccountState
{
    public void Deposit(BankAccount account, decimal amount)
    {
        throw new InvalidOperationException("Account is closed.");
    }

    public void Withdraw(BankAccount account, decimal amount)
    {
        throw new InvalidOperationException("Account is closed.");
    }

    public void Verify(BankAccount account)
    {
        throw new InvalidOperationException("Account is closed.");
    }

    public void Close(BankAccount account)
    {
        // Already closed — no further action needed
    }

    public void CheckForDeactivation(BankAccount account, TimeSpan inactivePeriod)
    {
        // Already closed — no further action needed
    }
}