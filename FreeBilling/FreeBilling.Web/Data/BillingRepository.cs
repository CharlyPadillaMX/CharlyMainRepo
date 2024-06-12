using FreeBilling.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FreeBilling.Web.Data
{
    public class BillingRepository : IBillingRepository
    {
        private readonly BillingContext _context;
        private readonly ILogger<BillingRepository> _logger;

        public BillingRepository(BillingContext context, ILogger<BillingRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Employee>> GetEmployeesAsync()
        {
            try
            {
                return await _context.Employees
                        .OrderBy(e => e.Name)
                        .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Could not get Employees: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<Customer>> GetCustomersAsync()
        {
            try
            {
                //throw new InvalidOperationException("Bad things happen to good developers");

                return await _context.Customers
                        .OrderBy(c => c.CompanyName)
                        .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Could not get Customers: {ex.Message}");
                throw;
            }
        }

        public async Task<Customer?> GetCustomersAsync(int id)
        {
            try
            {
                //throw new InvalidOperationException("Bad things happen to good developers");

                return await _context.Customers
                        .Where(c => c.Id == id)
                        .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Could not get Customers: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> SaveChanges()
        {
            try
            {
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Could not Save to the Database: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<Customer>> GetCustomersWithAddressesAsync()
        {
            try
            {
                return await _context.Customers
                        .Include(c => c.Address)
                        .OrderBy(c => c.CompanyName)
                        .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Could not get Customers: {ex.Message}");
                throw;
            }
        }

        public async Task<TimeBill?> GetTimeBill(int id)
        {
            var bill = await _context.TimeBills
                .Include(b => b.Employee)
                .Include(b => b.Customer)
                .ThenInclude(c => c!.Address)
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();

            return bill;
        }

        public void AddEntity<T>(T entity) where T : notnull
        {
            _context.Add(entity);
        }

        public async Task<IEnumerable<TimeBill>> GetTimeBillsForCustomer(int id)
        {
            return await _context.TimeBills
                .Where(c => c.CustomerId != null && c.CustomerId == id)
                .Include(b => b.Customer)
                .Include(b => b.Employee)
                .ToListAsync();
        }

        public async Task<TimeBill?> GetTimeBillsForCustomer(int id, int billId)
        {
            return await _context.TimeBills
                .Where(c => c.CustomerId != null && c.CustomerId == id && c.Id == billId)
                .Include(b => b.Customer)
                .Include(b => b.Employee)
                .FirstOrDefaultAsync();
        }

        public async Task<Employee?> GetEmployee(string? name)
        {
            return await _context.Employees
                .Where(c => c.Email == name)
                .FirstOrDefaultAsync();
        }
    }
}
