using BankingTransactionLoanManagementSystem.Data;
using BankingTransactionLoanManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace BankingTransactionLoanManagementSystem.Services;

public class ReportService : IReportService
{
    private readonly ApplicationDbContext _context;

    public ReportService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<DashboardViewModel> GetDashboardAsync()
    {
        return new DashboardViewModel
        {
            Customers = await _context.Customers.CountAsync(),
            Accounts = await _context.Accounts.CountAsync(),
            Loans = await _context.Loans.CountAsync(),
            Transactions = await _context.Transactions.CountAsync(),
            TotalBalances = await _context.Accounts.SumAsync(a => (decimal?)a.Balance) ?? 0,
            TotalLoans = await _context.Loans.SumAsync(l => (decimal?)l.LoanAmount) ?? 0
        };
    }

    public async Task<List<AuditLog>> GetAuditLogsAsync()
    {
        return await _context.AuditLogs.Include(a => a.Transaction).OrderByDescending(a => a.LogDate).ToListAsync();
    }
}
