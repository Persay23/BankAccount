namespace BankAccountProject.BankAccount.States;

public class DeactivatedState : IAccountState
{
    public void Deposit(BankAccount account, decimal amount)
    {
        account.State = new ActiveState();
        account.Reactivate();
        account.ChangeBalance(amount);
        account.UpdateLastOperationTime();
    }

    public void Withdraw(BankAccount account, decimal amount)
    {
        account.State = new ActiveState();
        account.Reactivate();

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
        // Already deactivated — no further action needed
    }
}