
using BankingSystem.Data;
using BankingSystem.Repositories;
using BankingSystem.Repositories.Interfaces;
using BankingSystem.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false)
    .Build();

var connectionString =
    configuration.GetConnectionString("BankingConnection")
    ?? throw new InvalidOperationException(
        "BankingConnection not found.");

var services = new ServiceCollection();

services.AddDbContext<BankingDbContext>(options =>
    options.UseSqlServer(connectionString));

services.AddScoped<ICustomerRepository, CustomerRepository>();
services.AddScoped<IBankAccountRepository, BankAccountRepository>();

services.AddScoped<CustomerService>();
services.AddScoped<BankAccountService>();

using var provider = services.BuildServiceProvider();

var running = true;

while (running)
{
    using var scope = provider.CreateScope();

    var customerService =
        scope.ServiceProvider.GetRequiredService<CustomerService>();

    var accountService =
        scope.ServiceProvider.GetRequiredService<BankAccountService>();

    Console.WriteLine("\n===== BANKING TRANSACTION MANAGEMENT =====");
    Console.WriteLine("1. Register customer");
    Console.WriteLine("2. View all customers");
    Console.WriteLine("3. Create bank account");
    Console.WriteLine("4. View all bank accounts");
    Console.WriteLine("5. View accounts by customer ID");
    Console.WriteLine("0. Exit");
    Console.Write("Choose an option: ");

    var choice = Console.ReadLine()?.Trim();

    try
    {
        switch (choice)
        {
            case "1":
                Console.Write("Full name: ");
                var name = Console.ReadLine() ?? "";

                Console.Write("Email: ");
                var email = Console.ReadLine() ?? "";

                Console.Write("Phone: ");
                var phone = Console.ReadLine() ?? "";

                await customerService.RegisterCustomerAsync(
                    name, email, phone);
                break;

            case "2":
                await customerService.DisplayCustomersAsync();
                break;

            case "3":
                Console.Write("Enter customer ID: ");

                if (!int.TryParse(Console.ReadLine(), out var customerId)
                    || customerId <= 0)
                {
                    Console.WriteLine("Enter a valid positive customer ID.");
                    break;
                }

                Console.Write("Enter account number: ");
                var accountNumber = Console.ReadLine() ?? "";

                await accountService.CreateAccountAsync(
                    customerId, accountNumber);
                break;

            case "4":
                await accountService.DisplayAllAccountsAsync();
                break;

            case "5":
                Console.Write("Enter customer ID: ");

                if (!int.TryParse(Console.ReadLine(), out var searchCustomerId)
                    || searchCustomerId <= 0)
                {
                    Console.WriteLine("Enter a valid positive customer ID.");
                    break;
                }

                await accountService.DisplayCustomerAccountsAsync(
                    searchCustomerId);
                break;

            case "0":
                running = false;
                Console.WriteLine("Application closed.");
                break;

            default:
                Console.WriteLine("Invalid option.");
                break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}
