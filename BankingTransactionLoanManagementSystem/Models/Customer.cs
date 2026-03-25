namespace BankingTransactionLoanManagementSystem.Models;

public class Customer
{
    public int CustomerId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? ContactInfo { get; set; }

    public ICollection<Account> Accounts { get; set; } = new List<Account>();
    public ICollection<Loan> Loans { get; set; } = new List<Loan>();
}
