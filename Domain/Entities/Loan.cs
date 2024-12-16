namespace Domain.Entities;

public class Loan
{
    public int LoanId { get; set; }
    public decimal LoanAmount { get; set; }
    public DateTime DateIssued { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public int CustomerId { get; set; }
    public int BranchId { get; set; }
}