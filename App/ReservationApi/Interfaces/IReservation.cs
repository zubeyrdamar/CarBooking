using ReservationApi.Models;

namespace ReservationApi.Interfaces
{
    public interface IReservation
    {
        Task<List<Reservation>> List();
        Task UpdateMailStatus(int id);
    }
}
