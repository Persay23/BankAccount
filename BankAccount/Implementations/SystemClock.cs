namespace BankAccountProject.Implementations;

using System;
using BankAccountProject.Interfaces;

public class SystemClock : IClock
{
    public DateTime UtcNow => DateTime.UtcNow;
}