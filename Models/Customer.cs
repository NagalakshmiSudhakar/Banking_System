
using System.ComponentModel.DataAnnotations;

namespace BankingSystem.Models;

public class Customer
{
    public int CustomerId { get; set; }

    [Required]
    [MaxLength(100)]
    public string FullName { get; set; } = string.Empty;

    [Required]
    [MaxLength(150)]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [MaxLength(15)]
    public string Phone { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<BankAccount> Accounts { get; set; }
        = new List<BankAccount>();
}
