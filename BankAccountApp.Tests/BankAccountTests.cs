using NUnit.Framework;

[TestFixture]
public class BankAccountTests
{
	private BankAccount account;

	[SetUp]
	public void Setup()
	{
		account = new BankAccount();
	}

	[Test]
	public void Deposit_IncreasesBalance()
	{
		account.Deposit(100);
		Assert.AreEqual(100, account.Balance);
	}

	[Test]
	public void Withdraw_WithoutVerification_Throws()
	{
		account.Deposit(100);
		Assert.Throws<UnauthorizedAccessException>(() => account.Withdraw(50));
	}

	[Test]
	public void Withdraw_WithVerification_DecreasesBalance()
	{
		account.Deposit(100);
		account.Verify();
		account.Withdraw(50);
		Assert.AreEqual(50, account.Balance);
	}

	[Test]
	public void CloseAccount_PreventsFurtherOperations()
	{
		account.Close();
		Assert.Throws<InvalidOperationException>(() => account.Deposit(50));
		Assert.Throws<InvalidOperationException>(() => account.Withdraw(10));
	}

	[Test]
	public void Account_Deactivates_AfterInactivity()
	{
		account.Deposit(100);
		account.CheckForDeactivation(TimeSpan.FromDays(-1)); // simulate long inactivity
		Assert.IsTrue(account.IsDeactivated);
	}

	[Test]
	public void Account_Reactivates_OnOperation()
	{
		bool wasReactivated = false;
		account.OnReactivated += () => wasReactivated = true;

		account.Deposit(100);
		account.CheckForDeactivation(TimeSpan.FromMilliseconds(-1)); // simulate long inactivity
		account.Deposit(10); // this should trigger reactivation

		Assert.IsFalse(account.IsDeactivated);
		Assert.IsTrue(wasReactivated);
	}
}
