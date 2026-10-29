
using BankingSystem.Models;

namespace BankingSystem.Repositories.Interfaces;

public interface ICustomerRepository
{
    Task AddAsync(Customer customer);

    Task<Customer?> GetByIdAsync(int customerId);

    Task<List<Customer>> GetAllAsync();

    Task<bool> EmailExistsAsync(string email);
}
