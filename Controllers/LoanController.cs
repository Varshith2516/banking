using BankingTransactionLoanManagementSystem.Models;
using BankingTransactionLoanManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankingTransactionLoanManagementSystem.Controllers;

public class LoanController : Controller
{
    private readonly ILoanService _loanService;

    public LoanController(ILoanService loanService)
    {
        _loanService = loanService;
    }

    public async Task<IActionResult> Index() => View(await _loanService.GetLoanDetailsAsync());

    public IActionResult ApplyLoan() => View(new LoanApplyViewModel());

    [HttpPost]
    public async Task<IActionResult> ApplyLoan(LoanApplyViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        await _loanService.ApplyLoanAsync(model);
        TempData["Success"] = "Loan application submitted.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> ApproveLoan(int id)
    {
        await _loanService.ApproveLoanAsync(id);
        TempData["Success"] = "Loan approved.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> GetLoanDetails()
    {
        return View("Index", await _loanService.GetLoanDetailsAsync());
    }
}
