
using BankingSystem.Models;

namespace BankingSystem.Repositories.Interfaces;

public interface IBankAccountRepository
{
    Task AddAsync(BankAccount account);

    Task<BankAccount?> GetByIdAsync(int accountId);

    Task<BankAccount?> GetByAccountNumberAsync(string accountNumber);

    Task<List<BankAccount>> GetAllAsync();

    Task<List<BankAccount>> GetByCustomerIdAsync(int customerId);

    Task<bool> AccountNumberExistsAsync(string accountNumber);

    Task UpdateAsync(BankAccount account);
}
