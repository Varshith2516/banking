using BankingTransactionLoanManagementSystem.Models;
using BankingTransactionLoanManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankingTransactionLoanManagementSystem.Controllers;

public class RepaymentController : Controller
{
    private readonly IRepaymentService _repaymentService;

    public RepaymentController(IRepaymentService repaymentService)
    {
        _repaymentService = repaymentService;
    }

    public IActionResult RecordRepayment() => View(new RepaymentViewModel());

    [HttpPost]
    public async Task<IActionResult> RecordRepayment(RepaymentViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        if (!await _repaymentService.RecordRepaymentAsync(model.LoanId, model.AmountPaid))
            ModelState.AddModelError("", "Repayment failed. Loan may not exist or may not be approved.");
        else
            TempData["Success"] = "Repayment recorded successfully.";
        return ModelState.IsValid && !ViewData.ModelState.ContainsKey("") ? RedirectToAction("Index", "Loan") : View(model);
    }

    public async Task<IActionResult> CalculateInterest(int id)
    {
        var interest = await _repaymentService.CalculateInterestAsync(id);
        if (interest == null) return NotFound();
        ViewBag.LoanId = id;
        ViewBag.Interest = interest;
        return View();
    }

    public async Task<IActionResult> GetRepaymentHistory(int id)
    {
        ViewBag.LoanId = id;
        return View(await _repaymentService.GetRepaymentHistoryAsync(id));
    }
}
