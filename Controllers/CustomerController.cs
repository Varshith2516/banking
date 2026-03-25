using BankingTransactionLoanManagementSystem.Models;
using BankingTransactionLoanManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankingTransactionLoanManagementSystem.Controllers;

public class CustomerController : Controller
{
    private readonly ICustomerService _customerService;

    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _customerService.GetCustomersAsync());
    }

    public IActionResult CreateAccount() => View(new CustomerAccountViewModel());

    [HttpPost]
    public async Task<IActionResult> CreateAccount(CustomerAccountViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        await _customerService.CreateAccountAsync(model);
        TempData["Success"] = "Customer and account created successfully.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> GetAccountDetails(int id)
    {
        var customer = await _customerService.GetCustomerDetailsAsync(id);
        if (customer == null) return NotFound();
        return View(customer);
    }

    public async Task<IActionResult> UpdateCustomerInfo(int id)
    {
        var customer = await _customerService.GetCustomerDetailsAsync(id);
        if (customer == null) return NotFound();
        return View(customer);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateCustomerInfo(Customer model)
    {
        if (!ModelState.IsValid) return View(model);
        await _customerService.UpdateCustomerInfoAsync(model);
        TempData["Success"] = "Customer updated successfully.";
        return RedirectToAction(nameof(Index));
    }
}
