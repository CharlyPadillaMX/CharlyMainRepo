using FreeBilling.Data.Entities;

namespace FreeBilling.Web.Data
{
    public interface IBillingRepository
    {
        Task<IEnumerable<Customer>> GetCustomersAsync();
        Task<IEnumerable<Customer>> GetCustomersWithAddressesAsync();
        Task<Customer?> GetCustomersAsync(int id);
        Task<IEnumerable<Employee>> GetEmployeesAsync();
        Task<bool> SaveChanges();
        Task<TimeBill?> GetTimeBill(int id);
        void AddEntity<T>(T entity) where T : notnull;
        Task<IEnumerable<TimeBill>> GetTimeBillsForCustomer(int id);
        Task<TimeBill?> GetTimeBillsForCustomer(int id, int billId);
        Task<Employee?> GetEmployee(string? name);
    }
}