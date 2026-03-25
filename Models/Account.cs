namespace BankingTransactionLoanManagementSystem.Models;

public class Account
{
    public int AccountId { get; set; }
    public int CustomerId { get; set; }
    public AccountType AccountType { get; set; }
    public decimal Balance { get; set; }

    public Customer? Customer { get; set; }
    public ICollection<TransactionRecord> Transactions { get; set; } = new List<TransactionRecord>();
}
