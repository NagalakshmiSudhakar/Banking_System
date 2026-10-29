
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace BankingSystem.Models;

public class BankTransaction
{
    [Key]
    public int TransactionId { get; set; }

    public int AccountId { get; set; }

    public BankAccount? Account { get; set; }

    public TransactionType TransactionType { get; set; }

    [Precision(18, 2)]
    public decimal Amount { get; set; }

    [Precision(18, 2)]
    public decimal BalanceAfterTransaction { get; set; }

    [MaxLength(250)]
    public string? Description { get; set; }

    public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
}
