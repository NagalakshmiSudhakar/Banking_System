
using BankingSystem.Models;
using BankingSystem.Repositories.Interfaces;

namespace BankingSystem.Services;

public class BankAccountService
{
    private readonly IBankAccountRepository _accountRepository;
    private readonly ICustomerRepository _customerRepository;

    public BankAccountService(
        IBankAccountRepository accountRepository,
        ICustomerRepository customerRepository)
    {
        _accountRepository = accountRepository;
        _customerRepository = customerRepository;
    }

    public async Task CreateAccountAsync(
        int customerId,
        string accountNumber)
    {
        if (customerId <= 0)
        {
            throw new ArgumentException(
                "Customer ID must be a positive number.");
        }

        if (string.IsNullOrWhiteSpace(accountNumber))
        {
            throw new ArgumentException(
                "Account number cannot be empty.");
        }

        accountNumber = accountNumber.Trim();

        if (accountNumber.Length > 20)
        {
            throw new ArgumentException(
                "Account number cannot exceed 20 characters.");
        }

        var customer =
            await _customerRepository.GetByIdAsync(customerId);

        if (customer is null)
        {
            throw new InvalidOperationException(
                $"Customer with ID {customerId} was not found.");
        }

        if (await _accountRepository
                .AccountNumberExistsAsync(accountNumber))
        {
            throw new InvalidOperationException(
                "This account number already exists.");
        }

        var account = new BankAccount
        {
            AccountNumber = accountNumber,
            CustomerId = customerId,
            Balance = 0,
            Status = AccountStatus.Active,
            CreatedAt = DateTime.UtcNow
        };

        await _accountRepository.AddAsync(account);

        Console.WriteLine(
            $"Bank account created successfully. Account ID: {account.AccountId}");
    }

    public async Task DisplayAllAccountsAsync()
    {
        var accounts = await _accountRepository.GetAllAsync();

        if (accounts.Count == 0)
        {
            Console.WriteLine("No bank accounts found.");
            return;
        }

        Console.WriteLine("\n===== BANK ACCOUNTS =====");

        foreach (var account in accounts)
        {
            Console.WriteLine($"Account ID   : {account.AccountId}");
            Console.WriteLine($"Account No.  : {account.AccountNumber}");
            Console.WriteLine($"Customer     : {account.Customer?.FullName ?? "Unknown"}");
            Console.WriteLine($"Customer ID  : {account.CustomerId}");
            Console.WriteLine($"Balance      : {account.Balance:F2}");
            Console.WriteLine($"Status       : {account.Status}");
            Console.WriteLine($"Created At   : {account.CreatedAt:yyyy-MM-dd HH:mm}");
            Console.WriteLine("------------------------------");
        }
    }

    public async Task DisplayCustomerAccountsAsync(int customerId)
    {
        if (customerId <= 0)
        {
            throw new ArgumentException(
                "Customer ID must be a positive number.");
        }

        var customer =
            await _customerRepository.GetByIdAsync(customerId);

        if (customer is null)
        {
            throw new InvalidOperationException(
                $"Customer with ID {customerId} was not found.");
        }

        var accounts =
            await _accountRepository.GetByCustomerIdAsync(customerId);

        if (accounts.Count == 0)
        {
            Console.WriteLine(
                $"Customer {customer.FullName} has no bank accounts.");
            return;
        }

        Console.WriteLine(
            $"\n===== ACCOUNTS FOR {customer.FullName} =====");

        foreach (var account in accounts)
        {
            Console.WriteLine(
                $"Account ID: {account.AccountId} | " +
                $"Number: {account.AccountNumber} | " +
                $"Balance: {account.Balance:F2} | " +
                $"Status: {account.Status}");
        }
    }


    public async Task FreezeAccountAsync(int accountId)
    {
        if (accountId <= 0)
        {
            throw new ArgumentException(
                "Account ID must be a positive number.");
        }

        var account = await _accountRepository.GetByIdAsync(accountId);

        if (account is null)
        {
            throw new InvalidOperationException(
                $"Account with ID {accountId} was not found.");
        }

        if (account.Status == AccountStatus.Frozen)
        {
            throw new InvalidOperationException(
                "This account is already frozen.");
        }

        if (account.Status == AccountStatus.Closed)
        {
            throw new InvalidOperationException(
                "A closed account cannot be frozen.");
        }

        account.Status = AccountStatus.Frozen;

        await _accountRepository.UpdateAsync(account);

        Console.WriteLine(
            $"Account {account.AccountNumber} has been frozen successfully.");
    }

    public async Task UnfreezeAccountAsync(int accountId)
    {
        if (accountId <= 0)
        {
            throw new ArgumentException(
                "Account ID must be a positive number.");
        }

        var account = await _accountRepository.GetByIdAsync(accountId);

        if (account is null)
        {
            throw new InvalidOperationException(
                $"Account with ID {accountId} was not found.");
        }

        if (account.Status == AccountStatus.Active)
        {
            throw new InvalidOperationException(
                "This account is already active.");
        }

        if (account.Status == AccountStatus.Closed)
        {
            throw new InvalidOperationException(
                "A closed account cannot be reactivated.");
        }

        account.Status = AccountStatus.Active;

        await _accountRepository.UpdateAsync(account);

        Console.WriteLine(
            $"Account {account.AccountNumber} has been unfrozen successfully.");
    }

}
