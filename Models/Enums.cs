namespace BankingTransactionLoanManagementSystem.Models;

public enum AccountType
{
    SAVINGS,
    CURRENT
}

public enum TransactionType
{
    DEPOSIT,
    WITHDRAWAL,
    TRANSFER
}

public enum LoanStatus
{
    APPLIED,
    APPROVED,
    REJECTED
}
