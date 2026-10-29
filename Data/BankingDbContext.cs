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
}