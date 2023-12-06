using Microsoft.EntityFrameworkCore;
using VehicleApi.Data;
using VehicleApi.Interfaces;
using VehicleApi.Models;

namespace VehicleApi.Services
{
    public class VehicleService : IVehicle
    {

        private ApiDbContext _context;
        public VehicleService() {
            _context = new ApiDbContext();
        }

        public async Task<List<Vehicle>> List()
        {
            return await _context.Vehicles.ToListAsync();
        }

        public async Task Create(Vehicle vehicle)
        {
            await _context.Vehicles.AddAsync(vehicle);
        }

        public async Task<Vehicle> Read(int id)
        {
            return await _context.Vehicles.FindAsync(id) ?? new Vehicle();
        }

        public async Task Update(int id, Vehicle vehicle)
        {
            var tempVehicle = await _context.Vehicles.FindAsync(id);
            if(tempVehicle != null)
            {
                tempVehicle.Name = vehicle.Name;
                tempVehicle.Price = vehicle.Price;
                tempVehicle.Displacement = vehicle.Displacement;
                tempVehicle.MaxSpeed = vehicle.MaxSpeed;
                tempVehicle.Length = vehicle.Length;
                tempVehicle.Width = vehicle.Width;
                tempVehicle.Height = vehicle.Height;

                await _context.SaveChangesAsync();
            }
        }

        public async Task Delete(int id)
        {
            var tempVehicle = await _context.Vehicles.FindAsync(id);
            if(tempVehicle != null )
            {
                _context.Vehicles.Remove(tempVehicle);
                await _context.SaveChangesAsync();
            }
        }
    }
}
