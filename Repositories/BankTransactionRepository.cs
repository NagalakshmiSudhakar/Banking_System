
using BankingSystem.Data;
using BankingSystem.Models;
using BankingSystem.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BankingSystem.Repositories;

public class BankTransactionRepository : IBankTransactionRepository
{
    private readonly BankingDbContext _context;

    public BankTransactionRepository(BankingDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(BankTransaction transaction)
    {
        _context.Set<BankTransaction>().Add(transaction);
        await _context.SaveChangesAsync();
    }

    public async Task<BankTransaction?> GetByIdAsync(int transactionId)
    {
        return await _context.Set<BankTransaction>()
            .AsNoTracking()
            .FirstOrDefaultAsync(
                transaction => transaction.TransactionId == transactionId);
    }

    public async Task<List<BankTransaction>> GetByAccountIdAsync(
        int accountId)
    {
        return await _context.Set<BankTransaction>()
            .AsNoTracking()
            .Where(transaction => transaction.AccountId == accountId)
            .OrderByDescending(transaction => transaction.TransactionDate)
            .ToListAsync();
    }

    public async Task<List<BankTransaction>> GetAllAsync()
    {
        return await _context.Set<BankTransaction>()
            .AsNoTracking()
            .OrderByDescending(transaction => transaction.TransactionDate)
            .ToListAsync();
    }
}
