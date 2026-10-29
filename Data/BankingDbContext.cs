
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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

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
    }
}
