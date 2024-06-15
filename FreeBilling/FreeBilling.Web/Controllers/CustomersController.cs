using FreeBilling.Data.Entities;
using FreeBilling.Web.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FreeBilling.Web.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("/api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ILogger<CustomersController> _logger;
        private readonly IBillingRepository _billingRepository;

        public CustomersController(IBillingRepository billingRepository, ILogger<CustomersController> logger)
        {
            _billingRepository = billingRepository;
            _logger = logger;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<Customer>>> Get(bool withAddresses = false)
        {
            try
            {
                IEnumerable<Customer> results = withAddresses ? await _billingRepository.GetCustomersWithAddressesAsync() : await _billingRepository.GetCustomersAsync();

                return Ok(results);
            }
            catch (Exception)
            {
                _logger.LogError("Failed to get customers from database");
                return Problem("Failed to get customers from database");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Customer>> GetOne(int id)
        {
            try
            {
                var result = await _billingRepository.GetCustomersAsync(id);

                if (result is null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception thrown while reading customer");
                return Problem($"Exception thrown: {ex.Message}");
            }
        }
    }
}
