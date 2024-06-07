using FreeBilling.Data.Entities;
using FreeBilling.Web.Data;
using Microsoft.AspNetCore.Mvc;

namespace FreeBilling.Web.Controllers
{
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
        public async Task<IEnumerable<Customer>> Get()
        {
            return await _billingRepository.GetCustomersAsync();
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
                _logger.LogError("Exception thrown while reading customer")
                return Problem($"Exception thrown: {ex.Message}");
            }
        }
    }
}
