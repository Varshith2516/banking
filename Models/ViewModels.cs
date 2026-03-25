using System.ComponentModel.DataAnnotations;

namespace BankingTransactionLoanManagementSystem.Models;

public class CustomerAccountViewModel
{
    [Required]
    public string Name { get; set; } = string.Empty;
    [EmailAddress]
    public string? Email { get; set; }
    public string? ContactInfo { get; set; }
    public AccountType AccountType { get; set; }
    [Range(0, double.MaxValue)]
    public decimal InitialBalance { get; set; }
}

public class DepositWithdrawViewModel
{
    [Required]
    public int AccountId { get; set; }
    [Range(0.01, double.MaxValue)]
    public decimal Amount { get; set; }
}

public class TransferViewModel
{
    [Required]
    public int FromAccountId { get; set; }
    [Required]
    public int ToAccountId { get; set; }
    [Range(0.01, double.MaxValue)]
    public decimal Amount { get; set; }
}

public class LoanApplyViewModel
{
    [Required]
    public int CustomerId { get; set; }
    [Range(1, double.MaxValue)]
    public decimal LoanAmount { get; set; }
    [Range(0, 100)]
    public decimal InterestRate { get; set; }
}

public class RepaymentViewModel
{
    [Required]
    public int LoanId { get; set; }
    [Range(0.01, double.MaxValue)]
    public decimal AmountPaid { get; set; }
}

public class DashboardViewModel
{
    public int Customers { get; set; }
    public int Accounts { get; set; }
    public int Loans { get; set; }
    public int Transactions { get; set; }
    public decimal TotalBalances { get; set; }
    public decimal TotalLoans { get; set; }
}
