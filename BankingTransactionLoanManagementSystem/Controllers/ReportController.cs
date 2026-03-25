using BankingTransactionLoanManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankingTransactionLoanManagementSystem.Controllers;

public class ReportController : Controller
{
    private readonly IReportService _reportService;

    public ReportController(IReportService reportService)
    {
        _reportService = reportService;
    }

    public async Task<IActionResult> GenerateTransactionReport()
    {
        return View("Dashboard", await _reportService.GetDashboardAsync());
    }

    public async Task<IActionResult> GetAuditLogs()
    {
        return View(await _reportService.GetAuditLogsAsync());
    }
}
