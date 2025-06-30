namespace BankAccount.Tests
{
    public class BankAccountTests
    {
        private readonly BankAccount account;
        private readonly FakeClock fakeClock;

        public class FakeClock : IClock
        {
            public DateTime UtcNow { get; set; } = DateTime.Now;
        }

        public BankAccountTests()
        {
            fakeClock = new FakeClock();
            account = new BankAccount(fakeClock);
        }

        [Fact]
        public void Deposit_IncreasesBalance()
        {
            account.Deposit(100);
            Assert.Equal(100, account.Balance);
        }

        [Fact]
        public void Withdraw_WithoutVerification_Throws()
        {
            account.Deposit(100);
            Assert.Throws<UnauthorizedAccessException>(() => account.Withdraw(50));
        }

        [Fact]
        public void Withdraw_WithVerification_DecreasesBalance()
        {
            account.Deposit(100);
            account.Verify();
            account.Withdraw(50);
            Assert.Equal(50, account.Balance);
        }

        [Fact]
        public void CloseAccount_PreventsFurtherOperations()
        {
            account.Close();
            Assert.Throws<InvalidOperationException>(() => account.Deposit(50));
            Assert.Throws<InvalidOperationException>(() => account.Withdraw(10));
        }

        [Fact]
        public void Account_Deactivates_AfterInactivity()
        {
            account.Deposit(100);
            fakeClock.UtcNow = fakeClock.UtcNow.AddDays(2);
            account.CheckForDeactivation(TimeSpan.FromDays(1));
            Assert.True(account.IsDeactivated);
        }

        [Fact]
        public void Account_Reactivates_OnOperation()
        {
            bool wasReactivated = false;
            account.OnReactivated += () => wasReactivated = true;

            account.Deposit(100);
            fakeClock.UtcNow = fakeClock.UtcNow.AddDays(2);
            account.CheckForDeactivation(TimeSpan.FromDays(1));
            account.Deposit(10);

            Assert.False(account.IsDeactivated);
            Assert.True(wasReactivated);
        }
    }
}