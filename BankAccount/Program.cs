namespace BankAccount;
internal static class Program
{
    static void Main()
    {
        // Create an instance of a clock implementation  
        var clock = new SystemClock(); 

        // Pass the clock instance to the BankAccount constructor  
        var account = new BankAccount(clock);

        // Subscribe to the reactivation event  
        account.OnReactivated += () =>
        {
            Console.WriteLine("Account has been reactivated!\n");
        };

        Console.WriteLine("Bank Account Demo\n");

        // 1. Depositing money  
        Console.WriteLine("Depositing 500");
        account.Deposit(500);
        Console.WriteLine($"Current Balance: {account.Balance}\n");

        // 2. Attempting to withdraw without verification  
        Console.WriteLine("Trying to withdraw 100 without verification\n");
        try
        {
            account.Withdraw(100);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        // 3. Verification of the user  
        Console.WriteLine("\nVerifying user\n");
        account.Verify();

        // 4. Withdrawal after verification  
        Console.WriteLine("Withdrawing 100");
        account.Withdraw(100);
        Console.WriteLine($"Current Balance: {account.Balance} \n");

        // 5. Deactivation after inactivity  
        Console.WriteLine("Simulating inactivity...");
        account.CheckForDeactivation(TimeSpan.FromDays(-1)); // simulate long inactivity  

        Console.WriteLine($"Is account deactivated? {account.IsDeactivated} \n");

        // 6. Operation that reactivates the account  
        Console.WriteLine("Depositing 50 to reactivate account");
        account.Deposit(50);

        Console.WriteLine($"Current Balance: {account.Balance}");
        Console.WriteLine($"Is account deactivated? {account.IsDeactivated} \n");

        // 7. Closing the account  
        Console.WriteLine("Closing account\n");
        account.Close();

        // 8. Attempt to withdraw after account is closed  
        Console.WriteLine("Trying to deposit after account is closed\n");
        try
        {
            account.Deposit(10);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error {ex.Message} \n");
        }

        Console.WriteLine("The End");
    }
}