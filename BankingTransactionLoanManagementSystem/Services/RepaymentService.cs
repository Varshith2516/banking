using BankingTransactionLoanManagementSystem.Data;
using BankingTransactionLoanManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace BankingTransactionLoanManagementSystem.Services;

public class RepaymentService : IRepaymentService
{
    private readonly ApplicationDbContext _context;

    public RepaymentService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> RecordRepaymentAsync(int loanId, decimal amountPaid)
    {
        var loan = await _context.Loans.Include(l => l.Repayments).FirstOrDefaultAsync(l => l.LoanId == loanId);
        if (loan == null || loan.LoanStatus != LoanStatus.APPROVED) return false;

        var totalWithInterest = loan.LoanAmount + ((loan.LoanAmount * loan.InterestRate) / 100m);
        var paidTillNow = loan.Repayments.Sum(r => r.AmountPaid);
        var balance = totalWithInterest - (paidTillNow + amountPaid);
        if (balance < 0) balance = 0;

        _context.Repayments.Add(new Repayment
        {
            LoanId = loanId,
            RepaymentDate = DateTime.UtcNow,
            AmountPaid = amountPaid,
            BalanceRemaining = balance
        });
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<decimal?> CalculateInterestAsync(int loanId)
    {
        var loan = await _context.Loans.FindAsync(loanId);
        if (loan == null) return null;
        return (loan.LoanAmount * loan.InterestRate) / 100m;
    }

    public async Task<List<Repayment>> GetRepaymentHistoryAsync(int loanId)
    {
        return await _context.Repayments.Where(r => r.LoanId == loanId).OrderByDescending(r => r.RepaymentDate).ToListAsync();
    }
}
