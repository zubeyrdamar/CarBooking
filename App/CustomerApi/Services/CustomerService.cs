using CustomerApi.Data;
using CustomerApi.Interfaces;
using CustomerApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerApi.Services
{
    public class CustomerService : ICustomer
    {

        private readonly ApiDbContext _context;
        public CustomerService()
        {
            _context = new ApiDbContext();
        }

        public async Task Create(Customer customer)
        {
            var tempVehicle = await _context.Vehicles.FirstOrDefaultAsync(o => o.Id == customer.Id);
            if (tempVehicle == null && customer.Vehicle != null)
            {
                await _context.Vehicles.AddAsync(customer.Vehicle);
                await _context.SaveChangesAsync();
            }

            customer.Vehicle = null;
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
        }
    }
}
