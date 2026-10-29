
using System.ComponentModel.DataAnnotations;
using BankingSystem.Models;
using BankingSystem.Repositories.Interfaces;

namespace BankingSystem.Services;

public class CustomerService
{
    private readonly ICustomerRepository _repository;

    public CustomerService(ICustomerRepository repository)
    {
        _repository = repository;
    }

    public async Task RegisterCustomerAsync(
        string fullName,
        string email,
        string phone)
    {
        fullName = fullName.Trim();
        email = email.Trim();
        phone = phone.Trim();

        if (string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentException("Name is required.");

        if (fullName.Length > 100)
            throw new ArgumentException("Name cannot exceed 100 characters.");

        if (string.IsNullOrWhiteSpace(email) ||
            email.Length > 150 ||
            !new EmailAddressAttribute().IsValid(email))
        {
            throw new ArgumentException("Enter a valid email address.");
        }

        if (string.IsNullOrWhiteSpace(phone) || phone.Length > 15)
            throw new ArgumentException("Enter a valid phone number.");

        if (await _repository.EmailExistsAsync(email))
            throw new InvalidOperationException(
                "A customer with this email already exists.");

        var customer = new Customer
        {
            FullName = fullName,
            Email = email,
            Phone = phone
        };

        await _repository.AddAsync(customer);

        Console.WriteLine(
            $"Customer registered successfully! ID: {customer.CustomerId}");
    }

    public async Task DisplayCustomersAsync()
    {
        var customers = await _repository.GetAllAsync();

        if (customers.Count == 0)
        {
            Console.WriteLine("No customers found.");
            return;
        }

        foreach (var customer in customers)
        {
            Console.WriteLine(
                $"ID: {customer.CustomerId} | " +
                $"Name: {customer.FullName} | " +
                $"Email: {customer.Email} | " +
                $"Phone: {customer.Phone}");
        }
    }
}
