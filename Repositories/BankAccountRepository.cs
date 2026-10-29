
using BankingSystem.Data;
using BankingSystem.Models;
using BankingSystem.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BankingSystem.Repositories;

public class BankAccountRepository : IBankAccountRepository
{
    private readonly BankingDbContext _context;

    public BankAccountRepository(BankingDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(BankAccount account)
    {
        _context.Set<BankAccount>().Add(account);
        await _context.SaveChangesAsync();
    }

    public async Task<BankAccount?> GetByIdAsync(int accountId)
    {
        return await _context.Set<BankAccount>()
            .Include(a => a.Customer)
            .FirstOrDefaultAsync(a => a.AccountId == accountId);
    }

    public async Task<BankAccount?> GetByAccountNumberAsync(
        string accountNumber)
    {
        return await _context.Set<BankAccount>()
            .FirstOrDefaultAsync(
                a => a.AccountNumber == accountNumber);
    }

    public async Task<List<BankAccount>> GetAllAsync()
    {
        return await _context.Set<BankAccount>()
            .Include(a => a.Customer)
            .AsNoTracking()
            .OrderBy(a => a.AccountId)
            .ToListAsync();
    }

    public async Task<List<BankAccount>> GetByCustomerIdAsync(
        int customerId)
    {
        return await _context.Set<BankAccount>()
            .Include(a => a.Customer)
            .AsNoTracking()
            .Where(a => a.CustomerId == customerId)
            .OrderBy(a => a.AccountId)
            .ToListAsync();
    }

    public async Task<bool> AccountNumberExistsAsync(
        string accountNumber)
    {
        return await _context.Set<BankAccount>()
            .AnyAsync(a => a.AccountNumber == accountNumber);
    }

    public async Task UpdateAsync(BankAccount account)
    {
        _context.BankAccounts.Update(account);
        await _context.SaveChangesAsync();
    }

}
