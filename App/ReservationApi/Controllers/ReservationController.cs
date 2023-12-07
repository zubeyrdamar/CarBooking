using Azure.Messaging.ServiceBus;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ReservationApi.Interfaces;
using ReservationApi.Models;

namespace ReservationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : Controller
    {

        private readonly IReservation _reservationService;
        public ReservationController(IReservation reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet]
        public async Task<IEnumerable<Reservation>> List()
        {
            return await _reservationService.List();
        }

        [HttpPut("{id}")]
        public async Task UpdateMailStatus(int id)
        {
            await _reservationService.UpdateMailStatus(id);
        }
    }
}
