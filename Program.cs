
using BankingSystem.Data;
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
        "BankingConnection not found in appsettings.json.");

var services = new ServiceCollection();

services.AddDbContext<BankingDbContext>(options =>
    options.UseSqlServer(connectionString));

using var provider = services.BuildServiceProvider();
using var scope = provider.CreateScope();

var dbContext =
    scope.ServiceProvider.GetRequiredService<BankingDbContext>();

try
{
    bool connected = await dbContext.Database.CanConnectAsync();

    Console.WriteLine(
        connected
            ? "Database connection successful!"
            : "Unable to connect to the database. Check your SQL Server instance.");
}
catch (Exception ex)
{
    Console.WriteLine($"Database connection failed: {ex.Message}");
}
