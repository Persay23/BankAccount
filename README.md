# Bank Account Management System

## Project Overview

This project implements a **Bank Account** class using the **State Pattern** to manage funds with key operations including deposits, withdrawals, account verification, deactivation, reactivation, and closure. It demonstrates core object-oriented programming and software engineering principles by encapsulating different account behaviors into distinct states.

The system models real-world banking requirements such as:

- Identity verification before withdrawals,
- Automatic account deactivation due to inactivity,
- Reactivation of accounts upon activity,
- Prevention of operations on closed accounts.

## Features

- **Deposit Funds:** Always allowed regardless of account state.
- **Withdraw Funds:** Allowed only after identity verification and only when the account is active.
- **Account Verification:** Simulates client identity verification before enabling withdrawals.
- **Account Closure:** Once closed, no operations (deposit/withdraw) are permitted.
- **Account Deactivation:** Automatically deactivates after a specified inactivity period.
- **Account Reactivation:** Reactivates on any operation (deposit or withdrawal), triggering a notification event.
- **State Management:** Different account behaviors are implemented in separate state classes (`ActiveState`, `DeactivatedState`, `ClosedState`), encapsulating logic per state.
- **Time Abstraction:** Uses an `IClock` interface to abstract time, allowing flexible testing with simulated time.

## Implementation Details

- Developed in **C#** as a console application.
- Applies the **State Design Pattern** to encapsulate account states and their behaviors.
- Uses **dependency injection** of `IClock` for managing time.
- Reactivation triggers an event (`OnReactivated`) that can be subscribed to for additional actions (e.g., notifications).
- Comprehensive **unit tests** implemented using **xUnit**, covering all functional scenarios and edge cases, including state transitions and error conditions.

## How to Run

1. Build the solution in Visual Studio or using the `dotnet build` command.
2. Run the console application project to see a demonstration of the bank account functionalities in action.
3. To execute tests, use Visual Studio Test Explorer or run `dotnet test` in the terminal.

## Project Structure

- **BankAccountApp** – main project containing the BankAccount class, state implementations, and console demo.
- **BankAccountApp.Tests** – unit tests project using xUnit and a fake clock implementation for time simulation.

## Usage

The console app creates a `BankAccount` instance with a real-time clock and demonstrates:

- Depositing funds in various account states.
- Attempting withdrawal without verification (throws an exception).
- Verifying the user and successfully withdrawing funds.
- Account inactivity leading to automatic deactivation.
- Account reactivation with event notification.
- Closing the account and blocking further operations.
