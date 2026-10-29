
using BankingSystem.Data;
using BankingSystem.Models;
using BankingSystem.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BankingSystem.Services;

public class BankTransactionService
{
    private readonly BankingDbContext _context;
    private readonly IBankAccountRepository _accountRepository;
    private readonly IBankTransactionRepository _transactionRepository;

    public BankTransactionService(
        BankingDbContext context,
        IBankAccountRepository accountRepository,
        IBankTransactionRepository transactionRepository)
    {
        _context = context;
        _accountRepository = accountRepository;
        _transactionRepository = transactionRepository;
    }

    public Task DepositAsync(int accountId, decimal amount)
    {
        return ProcessTransactionAsync(
            accountId, amount, TransactionType.Deposit);
    }

    public Task WithdrawAsync(int accountId, decimal amount)
    {
        return ProcessTransactionAsync(
            accountId, amount, TransactionType.Withdrawal);
    }

    private async Task ProcessTransactionAsync(
        int accountId,
        decimal amount,
        TransactionType transactionType)
    {
        if (accountId <= 0)
            throw new ArgumentException("Account ID must be positive.");

        if (amount <= 0)
            throw new ArgumentException("Amount must be greater than zero.");

        if (decimal.Round(amount, 2) != amount)
            throw new ArgumentException(
                "Amount cannot have more than two decimal places.");

        await using var dbTransaction =
            await _context.Database.BeginTransactionAsync();

        try
        {
            var account = await _accountRepository.GetByIdAsync(accountId);

            if (account is null)
                throw new InvalidOperationException("Bank account not found.");

            if (account.Status != AccountStatus.Active)
                throw new InvalidOperationException(
                    "Transactions are allowed only on active accounts.");

            if (transactionType == TransactionType.Withdrawal
                && account.Balance < amount)
            {
                throw new InvalidOperationException(
                    "Insufficient balance for withdrawal.");
            }

            var previousBalance = account.Balance;

            account.Balance = transactionType == TransactionType.Deposit
                ? account.Balance + amount
                : account.Balance - amount;

            _context.BankAccounts.Update(account);

            var transaction = new BankTransaction
            {
                AccountId = accountId,
                TransactionType = transactionType,
                Amount = amount,
                BalanceAfterTransaction = account.Balance,
                Description = transactionType == TransactionType.Deposit
                    ? "Cash deposit"
                    : "Cash withdrawal",
                TransactionDate = DateTime.UtcNow
            };

            _context.Set<BankTransaction>().Add(transaction);

            await _context.SaveChangesAsync();
            await dbTransaction.CommitAsync();

            Console.WriteLine(
                $"{transactionType} successful. Amount: {amount:F2}");
            Console.WriteLine(
                $"Previous balance: {previousBalance:F2}");
            Console.WriteLine(
                $"Current balance: {account.Balance:F2}");
            Console.WriteLine(
                $"Transaction ID: {transaction.TransactionId}");
        }
        catch
        {
            await dbTransaction.RollbackAsync();
            throw;
        }
    }

    public async Task DisplayTransactionHistoryAsync(int accountId)
    {
        if (accountId <= 0)
            throw new ArgumentException("Account ID must be positive.");

        var account = await _accountRepository.GetByIdAsync(accountId);

        if (account is null)
            throw new InvalidOperationException("Bank account not found.");

        var transactions =
            await _transactionRepository.GetByAccountIdAsync(accountId);

        if (transactions.Count == 0)
        {
            Console.WriteLine("No transactions found for this account.");
            return;
        }

        Console.WriteLine(
            $"\n===== TRANSACTION HISTORY: {account.AccountNumber} =====");

        foreach (var transaction in transactions)
        {
            Console.WriteLine($"Transaction ID : {transaction.TransactionId}");
            Console.WriteLine($"Type           : {transaction.TransactionType}");
            Console.WriteLine($"Amount         : {transaction.Amount:F2}");
            Console.WriteLine(
                $"Balance After  : {transaction.BalanceAfterTransaction:F2}");
            Console.WriteLine(
                $"Date           : {transaction.TransactionDate:yyyy-MM-dd HH:mm:ss} UTC");
            Console.WriteLine($"Description    : {transaction.Description}");
            Console.WriteLine("--------------------------------");
        }
    }
}
