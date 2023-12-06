using VehicleApi.Models;

namespace VehicleApi.Interfaces
{
    public interface IVehicle
    {
        Task<List<Vehicle>> List();
        Task<Vehicle> Read(int id);
        Task Create(Vehicle vehicle);
        Task Update(int id, Vehicle vehicle);
        Task Delete(int id);
    }
}
