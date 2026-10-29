
using BankingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace BankingSystem.Data;

public class BankingDbContext : DbContext
{
    public BankingDbContext(
        DbContextOptions<BankingDbContext> options)
        : base(options)
    {
    }

    public DbSet<Customer> Customers => Set<Customer>();

    public DbSet<BankAccount> BankAccounts => Set<BankAccount>();

    public DbSet<BankTransaction> BankTransactions =>
        Set<BankTransaction>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Bank account configuration
        modelBuilder.Entity<BankAccount>()
            .HasIndex(account => account.AccountNumber)
            .IsUnique();

        modelBuilder.Entity<BankAccount>()
            .Property(account => account.Balance)
            .HasPrecision(18, 2);

        modelBuilder.Entity<BankAccount>()
            .Property(account => account.RowVersion)
            .IsRowVersion();

        modelBuilder.Entity<BankAccount>()
            .HasOne(account => account.Customer)
            .WithMany(customer => customer.Accounts)
            .HasForeignKey(account => account.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        // Transaction configuration
        modelBuilder.Entity<BankTransaction>()
            .Property(transaction => transaction.Amount)
            .HasPrecision(18, 2);

        modelBuilder.Entity<BankTransaction>()
            .Property(transaction => transaction.BalanceAfterTransaction)
            .HasPrecision(18, 2);

        modelBuilder.Entity<BankTransaction>()
            .HasOne(transaction => transaction.Account)
            .WithMany()
            .HasForeignKey(transaction => transaction.AccountId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<BankTransaction>()
            .HasIndex(transaction => new
            {
                transaction.AccountId,
                transaction.TransactionDate
            });
    }
}
