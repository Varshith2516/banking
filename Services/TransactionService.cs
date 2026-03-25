using BankingTransactionLoanManagementSystem.Data;
using BankingTransactionLoanManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace BankingTransactionLoanManagementSystem.Services;

public class TransactionService : ITransactionService
{
    private readonly ApplicationDbContext _context;

    public TransactionService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> DepositFundsAsync(int accountId, decimal amount, string performedBy = "System")
    {
        var account = await _context.Accounts.FindAsync(accountId);
        if (account == null) return false;

        account.Balance += amount;
        var transaction = new TransactionRecord
        {
            AccountId = accountId,
            Amount = amount,
            TransactionType = TransactionType.DEPOSIT,
            TransactionDate = DateTime.UtcNow
        };
        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();
        await LogAsync(transaction.TransactionId, "Deposit performed", performedBy);
        return true;
    }

    public async Task<bool> WithdrawFundsAsync(int accountId, decimal amount, string performedBy = "System")
    {
        var account = await _context.Accounts.FindAsync(accountId);
        if (account == null || account.Balance < amount) return false;

        account.Balance -= amount;
        var transaction = new TransactionRecord
        {
            AccountId = accountId,
            Amount = amount,
            TransactionType = TransactionType.WITHDRAWAL,
            TransactionDate = DateTime.UtcNow
        };
        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();
        await LogAsync(transaction.TransactionId, "Withdrawal performed", performedBy);
        return true;
    }

    public async Task<bool> TransferFundsAsync(int fromAccountId, int toAccountId, decimal amount, string performedBy = "System")
    {
        if (fromAccountId == toAccountId) return false;

        var from = await _context.Accounts.FindAsync(fromAccountId);
        var to = await _context.Accounts.FindAsync(toAccountId);
        if (from == null || to == null || from.Balance < amount) return false;

        from.Balance -= amount;
        to.Balance += amount;

        var transaction = new TransactionRecord
        {
            AccountId = fromAccountId,
            Amount = amount,
            TransactionType = TransactionType.TRANSFER,
            TransactionDate = DateTime.UtcNow
        };

        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();
        await LogAsync(transaction.TransactionId, $"Transfer from {fromAccountId} to {toAccountId}", performedBy);
        return true;
    }

    public async Task<List<TransactionRecord>> GetTransactionsAsync()
    {
        return await _context.Transactions.Include(t => t.Account).OrderByDescending(t => t.TransactionDate).ToListAsync();
    }

    private async Task LogAsync(int transactionId, string action, string performedBy)
    {
        _context.AuditLogs.Add(new AuditLog
        {
            TransactionId = transactionId,
            LogDate = DateTime.UtcNow,
            ActionPerformed = action,
            PerformedBy = performedBy
        });
        await _context.SaveChangesAsync();
    }
}
