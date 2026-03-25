namespace BankingTransactionLoanManagementSystem.Models;

public class AuditLog
{
    public int LogId { get; set; }
    public int? TransactionId { get; set; }
    public DateTime LogDate { get; set; } = DateTime.UtcNow;
    public string ActionPerformed { get; set; } = string.Empty;
    public string PerformedBy { get; set; } = string.Empty;

    public TransactionRecord? Transaction { get; set; }
}
