using BankingTransactionLoanManagementSystem.Data;
using BankingTransactionLoanManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace BankingTransactionLoanManagementSystem.Services;

public class CustomerService : ICustomerService
{
    private readonly ApplicationDbContext _context;

    public CustomerService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Customer> CreateAccountAsync(CustomerAccountViewModel model)
    {
        var customer = new Customer
        {
            Name = model.Name,
            Email = model.Email,
            ContactInfo = model.ContactInfo,
            Accounts = new List<Account>
            {
                new Account
                {
                    AccountType = model.AccountType,
                    Balance = model.InitialBalance
                }
            }
        };

        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();
        return customer;
    }

    public async Task<List<Customer>> GetCustomersAsync()
    {
        return await _context.Customers.Include(c => c.Accounts).ToListAsync();
    }

    public async Task<Customer?> GetCustomerDetailsAsync(int customerId)
    {
        return await _context.Customers
            .Include(c => c.Accounts)
            .Include(c => c.Loans)
            .FirstOrDefaultAsync(c => c.CustomerId == customerId);
    }

    public async Task<bool> UpdateCustomerInfoAsync(Customer customer)
    {
        var existing = await _context.Customers.FindAsync(customer.CustomerId);
        if (existing == null) return false;

        existing.Name = customer.Name;
        existing.Email = customer.Email;
        existing.ContactInfo = customer.ContactInfo;
        await _context.SaveChangesAsync();
        return true;
    }
}
