namespace BankingTransactionLoanManagementSystem.Models;

public class Repayment
{
    public int RepaymentId { get; set; }
    public int LoanId { get; set; }
    public DateTime RepaymentDate { get; set; } = DateTime.UtcNow;
    public decimal AmountPaid { get; set; }
    public decimal BalanceRemaining { get; set; }

    public Loan? Loan { get; set; }
}
