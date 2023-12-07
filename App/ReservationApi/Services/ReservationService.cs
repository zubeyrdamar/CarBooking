using Azure.Messaging.ServiceBus;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ReservationApi.Data;
using ReservationApi.Interfaces;
using ReservationApi.Models;
using System.Net;
using System.Net.Mail;

namespace ReservationApi.Services
{
    public class ReservationService : IReservation
    {

        private readonly ApiDbContext _context;
        public ReservationService()
        {
            _context = new ApiDbContext();
        }

        public async Task<List<Reservation>> List()
        {
            // AZURE MESSAGING BUS 

            Console.WriteLine("Message Receiving...");

            string connectionString = "<Connection String>";
            string queueName = "<Queue Name>";
            // since ServiceBusClient implements IAsyncDisposable we create it with "await using"
            await using var client = new ServiceBusClient(connectionString);

            // create a receiver that we can use to receive the message
            ServiceBusReceiver receiver = client.CreateReceiver(queueName);

            // the received message is a different type as it contains some service set properties
            IReadOnlyList<ServiceBusReceivedMessage> receivedMessages = await receiver.ReceiveMessagesAsync(10);
            if (receivedMessages == null)
            {
                return null;
            }

            foreach (ServiceBusReceivedMessage receivedMessage in receivedMessages)
            {
                string body = receivedMessage.Body.ToString();
                var message = JsonConvert.DeserializeObject<Reservation>(body);
                if(message !=  null) {

                    await _context.Reservations.AddAsync(message);
                    await _context.SaveChangesAsync();
                    await receiver.CompleteMessageAsync(receivedMessage);
                }
            }

            // get the message body as a string
            Console.WriteLine("Messages Received.");

            return await _context.Reservations.ToListAsync();
        }

        public async Task UpdateMailStatus(int id)
        {
            var tempReservation = await _context.Reservations.FindAsync(id);
            if(tempReservation !=  null && tempReservation.IsMailSent == false) {

                var smtpClient = new SmtpClient("smtp.live.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("useremail@outlook.com", "EmailPassword"),
                    EnableSsl = true,
                };
                smtpClient.Send("useremail@outlook.com", tempReservation.Email, "Vehicle Test Drive", "Your test drive is reserved");
                tempReservation.IsMailSent = true;

                await _context.SaveChangesAsync();
            }
        }
    }
}
