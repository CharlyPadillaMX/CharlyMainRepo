using FreeBilling.Data.Entities;

namespace FreeBilling.Web.Data
{
    public interface IBillingRepository
    {
        Task<IEnumerable<Customer>> GetCustomersAsync();
        Task<Customer?> GetCustomersAsync(int id);
        Task<IEnumerable<Employee>> GetEmployeesAsync();
        Task<bool> SaveChanges();
    }
}