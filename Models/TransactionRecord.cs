namespace BankingTransactionLoanManagementSystem.Models;

public class TransactionRecord
{
    public int TransactionId { get; set; }
    public int AccountId { get; set; }
    public TransactionType TransactionType { get; set; }
    public decimal Amount { get; set; }
    public DateTime TransactionDate { get; set; } = DateTime.UtcNow;

    public Account? Account { get; set; }
    public ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();
}
