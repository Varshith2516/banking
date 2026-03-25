using BankingTransactionLoanManagementSystem.Models;

namespace BankingTransactionLoanManagementSystem.Services;

public interface ITransactionService
{
    Task<bool> DepositFundsAsync(int accountId, decimal amount, string performedBy = "System");
    Task<bool> WithdrawFundsAsync(int accountId, decimal amount, string performedBy = "System");
    Task<bool> TransferFundsAsync(int fromAccountId, int toAccountId, decimal amount, string performedBy = "System");
    Task<List<TransactionRecord>> GetTransactionsAsync();
}
