using System.ComponentModel.DataAnnotations;

namespace ReservationApi.Models
{
    public class Vehicle
    {
        [Key]
        public int VId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
}