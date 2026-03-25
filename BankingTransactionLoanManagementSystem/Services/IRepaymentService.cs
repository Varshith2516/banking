using BankingTransactionLoanManagementSystem.Models;

namespace BankingTransactionLoanManagementSystem.Services;

public interface IRepaymentService
{
    Task<bool> RecordRepaymentAsync(int loanId, decimal amountPaid);
    Task<decimal?> CalculateInterestAsync(int loanId);
    Task<List<Repayment>> GetRepaymentHistoryAsync(int loanId);
}
