namespace BankAccountProject.BankAccount;

public interface IAccountState
{
    void Deposit(BankAccount account, decimal amount);
    void Withdraw(BankAccount account, decimal amount);
    void Verify(BankAccount account);
    void Close(BankAccount account);
    void CheckForDeactivation(BankAccount account, TimeSpan inactivePeriod);
}