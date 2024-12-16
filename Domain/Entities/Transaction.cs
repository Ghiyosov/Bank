using Domain.Enums;

namespace Domain.Entities;

public class Transaction
{
    public int TransactionId { get; set; }
    public TransactionStatus TransactionStatus { get; set; }
    public DateTime DateIssued { get; set; }
    public decimal Amount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public int FromAccountId { get; set; }
    public int ToAccountId { get; set; }
}