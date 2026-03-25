using BankingTransactionLoanManagementSystem.Models;

namespace BankingTransactionLoanManagementSystem.Services;

public interface ICustomerService
{
    Task<Customer> CreateAccountAsync(CustomerAccountViewModel model);
    Task<List<Customer>> GetCustomersAsync();
    Task<Customer?> GetCustomerDetailsAsync(int customerId);
    Task<bool> UpdateCustomerInfoAsync(Customer customer);
}
