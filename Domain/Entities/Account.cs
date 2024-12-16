using Domain.Enums;

namespace Domain.Entities;

public class Account
{
    public int AccountId { get; set; }
    public decimal Balance { get; set; }
    public AccountStatus AccountStatus { get; set; }
    public AccountType AccountType { get; set; }
    public string Currency { get; set; } = string.Empty;
    public int Customerid { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}