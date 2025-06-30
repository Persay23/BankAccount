namespace BankAccount;
public class BankAccount(IClock clock)
{
    private decimal _balance = 0m;
    private bool _isClosed = false;
    private bool _isDeactivated = false;
    private bool _isVerified = false;
    private DateTime _lastOperationTime = clock.UtcNow;
    private readonly IClock _clock = clock;

    public event Action? OnReactivated;

    public void Deposit(decimal amount)
    {
        if (_isClosed)
            throw new InvalidOperationException("Account is closed.");

        ReactivateIfNeeded();
        _balance += amount;
        _lastOperationTime = _clock.UtcNow;
    }

    public void Verify()
    {
        _isVerified = true;
    }

    public void Withdraw(decimal amount)
    {
        if (_isClosed)
            throw new InvalidOperationException("Account is closed.");
        if (!_isVerified)
            throw new UnauthorizedAccessException("Client is not verified.");
        if (_balance < amount)
            throw new InvalidOperationException("Insufficient funds.");

        ReactivateIfNeeded();
        _balance -= amount;
        _lastOperationTime = _clock.UtcNow;
    }

    public void Close()
    {
        _isClosed = true;
    }

    public void CheckForDeactivation(TimeSpan inactivePeriod)
    {
        if (!_isClosed && _clock.UtcNow - _lastOperationTime > inactivePeriod)
        {
            _isDeactivated = true;
        }
    }

    private void ReactivateIfNeeded()
    {
        if (_isDeactivated)
        {
            _isDeactivated = false;
            OnReactivated?.Invoke();
        }
    }

    public decimal Balance => _balance;
    public bool IsClosed => _isClosed;
    public bool IsDeactivated => _isDeactivated;
    public bool IsVerified => _isVerified;
}

public interface IClock
{
    DateTime UtcNow { get; }
}

public class SystemClock : IClock
{
    public DateTime UtcNow => DateTime.UtcNow;
}