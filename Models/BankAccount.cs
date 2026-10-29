
using System.ComponentModel.DataAnnotations;

namespace BankingSystem.Models;

public class BankAccount
{
    [Key]
    public int AccountId { get; set; }

    [Required]
    [MaxLength(20)]
    public string AccountNumber { get; set; } = string.Empty;

    public int CustomerId { get; set; }

    public Customer? Customer { get; set; }

    public decimal Balance { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
