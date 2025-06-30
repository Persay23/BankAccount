# Bank Account Management System

## Project Overview

This project implements a simple **Bank Account** class designed to manage funds with key operations including deposits and withdrawals. It demonstrates core principles of object-oriented programming and software engineering, incorporating real-world requirements such as account verification, deactivation due to inactivity, reactivation, and account closure.

## Features

- **Deposit Funds:** Always allowed regardless of account state.
- **Withdraw Funds:** Allowed only after identity verification.
- **Account Verification:** Simulates client identity verification before withdrawal.
- **Account Closure:** Once closed, no operations are permitted.
- **Account Deactivation:** Automatically deactivates after a specified inactivity period.
- **Account Reactivation:** Reactivates on any operation (deposit or withdrawal), triggering a notification event.
- **Time Abstraction:** Uses an `IClock` interface to abstract time, allowing flexible testing with simulated time.

## Implementation Details

- Developed in C# as a console application.
- Uses dependency injection of `IClock` for time management.
- Reactivation triggers an event that can be subscribed to for additional actions.
- Comprehensive unit tests implemented using xUnit, covering all functional scenarios and edge cases.

## How to Run

1. Build the solution in Visual Studio or using the `dotnet build` command.
2. Run the console application project to see a demonstration of the bank account functionalities.
3. To execute tests, use Visual Studio Test Explorer or run `dotnet test` in the terminal.

## Project Structure

- `BankAccountApp` – main project with the bank account class and console demo.
- `BankAccountApp.Tests` – unit tests project using xUnit and a fake clock implementation for time simulation.

## Usage

The console app creates a `BankAccount` instance with real-time clock and demonstrates:

- Depositing funds
- Attempting withdrawal without verification (throws exception)
- Verifying user and successfully withdrawing funds
- Account inactivity and automatic deactivation
- Account reactivation with event notification
- Closing account and blocking further operations
