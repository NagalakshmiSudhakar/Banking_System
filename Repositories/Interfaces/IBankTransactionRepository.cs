
using BankingSystem.Models;

namespace BankingSystem.Repositories.Interfaces;

public interface IBankTransactionRepository
{
    Task AddAsync(BankTransaction transaction);

    Task<BankTransaction?> GetByIdAsync(int transactionId);

    Task<List<BankTransaction>> GetByAccountIdAsync(int accountId);

    Task<List<BankTransaction>> GetAllAsync();
}
