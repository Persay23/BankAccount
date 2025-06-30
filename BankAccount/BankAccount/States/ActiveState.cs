namespace BankAccountProject.BankAccount.States;

public class ActiveState : IAccountState
{
    public void Deposit(BankAccount account, decimal amount)
    {
        account.ChangeBalance(amount);
        account.UpdateLastOperationTime();
    }

    public void Withdraw(BankAccount account, decimal amount)
    {
        if (!account.IsVerified)
            throw new UnauthorizedAccessException("Client is not verified.");

        if (account.Balance < amount)
            throw new InvalidOperationException("Insufficient funds.");

        account.ChangeBalance(-amount);
        account.UpdateLastOperationTime();
    }

    public void Verify(BankAccount account)
    {
        account.IsVerified = true;
    }

    public void Close(BankAccount account)
    {
        account.State = new ClosedState();
    }

    public void CheckForDeactivation(BankAccount account, TimeSpan inactivePeriod)
    {
        if (account.LastOperationTime + inactivePeriod < account._clock.UtcNow)
        {
            account.State = new DeactivatedState();
            account.Reactivate();
        }
    }
}