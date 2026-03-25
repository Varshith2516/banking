using BankingTransactionLoanManagementSystem.Models;
using BankingTransactionLoanManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankingTransactionLoanManagementSystem.Controllers;

public class TransactionController : Controller
{
    private readonly ITransactionService _transactionService;

    public TransactionController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    public async Task<IActionResult> Index() => View(await _transactionService.GetTransactionsAsync());

    public IActionResult DepositFunds() => View(new DepositWithdrawViewModel());

    [HttpPost]
    public async Task<IActionResult> DepositFunds(DepositWithdrawViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        if (!await _transactionService.DepositFundsAsync(model.AccountId, model.Amount, "Admin"))
            ModelState.AddModelError("", "Deposit failed.");
        else
            TempData["Success"] = "Deposit successful.";
        return ModelState.IsValid ? RedirectToAction(nameof(Index)) : View(model);
    }

    public IActionResult WithdrawFunds() => View(new DepositWithdrawViewModel());

    [HttpPost]
    public async Task<IActionResult> WithdrawFunds(DepositWithdrawViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        if (!await _transactionService.WithdrawFundsAsync(model.AccountId, model.Amount, "Admin"))
            ModelState.AddModelError("", "Withdrawal failed. Check balance.");
        else
            TempData["Success"] = "Withdrawal successful.";
        return ModelState.IsValid && !ViewData.ModelState.ContainsKey("") ? RedirectToAction(nameof(Index)) : View(model);
    }

    public IActionResult TransferFunds() => View(new TransferViewModel());

    [HttpPost]
    public async Task<IActionResult> TransferFunds(TransferViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        if (!await _transactionService.TransferFundsAsync(model.FromAccountId, model.ToAccountId, model.Amount, "Admin"))
            ModelState.AddModelError("", "Transfer failed. Check account ids and balance.");
        else
            TempData["Success"] = "Transfer successful.";
        return ModelState.IsValid && !ViewData.ModelState.ContainsKey("") ? RedirectToAction(nameof(Index)) : View(model);
    }
}
