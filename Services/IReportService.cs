using BankingTransactionLoanManagementSystem.Models;

namespace BankingTransactionLoanManagementSystem.Services;

public interface IReportService
{
    Task<DashboardViewModel> GetDashboardAsync();
    Task<List<AuditLog>> GetAuditLogsAsync();
}
