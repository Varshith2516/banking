using BankingTransactionLoanManagementSystem.Data;
using BankingTransactionLoanManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace BankingTransactionLoanManagementSystem.Services;

public class LoanService : ILoanService
{
    private readonly ApplicationDbContext _context;

    public LoanService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Loan> ApplyLoanAsync(LoanApplyViewModel model)
    {
        var loan = new Loan
        {
            CustomerId = model.CustomerId,
            LoanAmount = model.LoanAmount,
            InterestRate = model.InterestRate,
            LoanStatus = LoanStatus.APPLIED
        };
        _context.Loans.Add(loan);
        await _context.SaveChangesAsync();
        return loan;
    }

    public async Task<bool> ApproveLoanAsync(int loanId)
    {
        var loan = await _context.Loans.FindAsync(loanId);
        if (loan == null) return false;
        loan.LoanStatus = LoanStatus.APPROVED;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<Loan>> GetLoanDetailsAsync()
    {
        return await _context.Loans.Include(l => l.Customer).Include(l => l.Repayments).ToListAsync();
    }
}
