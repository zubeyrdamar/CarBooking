using Microsoft.AspNetCore.Mvc;
using VehicleApi.Interfaces;
using VehicleApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VehicleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private IVehicle _vehicleService;
        public VehiclesController(IVehicle vehicleService)
        {
            _vehicleService = vehicleService;
        }

        // GET: api/<VehiclesController>
        [HttpGet]
        public async Task<IEnumerable<Vehicle>> List()
        {
            return await _vehicleService.List();
        }

        // GET api/<VehiclesController>/5
        [HttpGet("{id}")]
        public async Task<Vehicle> Read(int id)
        {
            return await _vehicleService.Read(id);
        }

        // POST api/<VehiclesController>
        [HttpPost]
        public async Task Create([FromBody] Vehicle vehicle)
        {
            await _vehicleService.Create(vehicle);
        }

        // PUT api/<VehiclesController>/5
        [HttpPut("{id}")]
        public async Task Update(int id, [FromBody] Vehicle vehicle)
        {
            await _vehicleService.Update(id, vehicle);
        }

        // DELETE api/<VehiclesController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _vehicleService.Delete(id);
        }
    }
}
