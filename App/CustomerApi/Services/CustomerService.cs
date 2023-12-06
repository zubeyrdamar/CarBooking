using Azure.Messaging.ServiceBus;
using CustomerApi.Data;
using CustomerApi.Interfaces;
using CustomerApi.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

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

            // AZURE MESSAGING BUS 

            Console.WriteLine("Message Sending...");
            
            string connectionString = "Endpoint = sb://carbookingtestdrive.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=NPLm3gQZXkoctCug1UheOiu1+0aqZ/RFz+ASbBKGWp0=";
            string queueName = "azureorderqueue";
            // since ServiceBusClient implements IAsyncDisposable we create it with "await using"
            await using var client = new ServiceBusClient(connectionString);
            
            var customerJson = JsonConvert.SerializeObject(customer);

            // create the sender
            ServiceBusSender sender = client.CreateSender(queueName);

            // create a message that we can send. UTF-8 encoding is used when providing a string.
            ServiceBusMessage message = new ServiceBusMessage(customerJson);

            // send the message
            await sender.SendMessageAsync(message);

            Console.WriteLine("Message has been sent.");
        }
    }
}
