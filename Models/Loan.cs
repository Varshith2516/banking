namespace BankingTransactionLoanManagementSystem.Models;

public class Loan
{
    public int LoanId { get; set; }
    public int CustomerId { get; set; }
    public decimal LoanAmount { get; set; }
    public decimal InterestRate { get; set; }
    public LoanStatus LoanStatus { get; set; } = LoanStatus.APPLIED;

    public Customer? Customer { get; set; }
    public ICollection<Repayment> Repayments { get; set; } = new List<Repayment>();
}
