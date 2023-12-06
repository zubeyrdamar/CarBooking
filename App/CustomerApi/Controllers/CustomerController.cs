using CustomerApi.Interfaces;
using CustomerApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CustomerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private readonly ICustomer _customerService;
        public CustomerController(ICustomer customerService)
        {
            _customerService = customerService;
        }


        [HttpPost]
        public async Task Create([FromBody] Customer customer)
        {
            await _customerService.Create(customer);
        }
    }
}
