using BankingTransactionLoanManagementSystem.Models;

namespace BankingTransactionLoanManagementSystem.Services;

public interface ILoanService
{
    Task<Loan> ApplyLoanAsync(LoanApplyViewModel model);
    Task<bool> ApproveLoanAsync(int loanId);
    Task<List<Loan>> GetLoanDetailsAsync();
}
