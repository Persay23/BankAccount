namespace BankAccountProject.Interfaces;

using System;

public interface IClock
{
    DateTime UtcNow { get; }
}