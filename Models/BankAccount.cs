
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

    public decimal Balance { get; set; } = 0;

    public AccountStatus Status { get; set; } = AccountStatus.Active;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Timestamp]
    public byte[] RowVersion { get; set; } = Array.Empty<byte>();
}
