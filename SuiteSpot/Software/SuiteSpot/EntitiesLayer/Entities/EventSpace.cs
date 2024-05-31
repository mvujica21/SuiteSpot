using System.Collections.Generic;

namespace HotelManagement.Entities
{
    public class EventSpace
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Capacity { get; set; }
        public string Amenities { get; set; }
        public decimal PricePerHour { get; set; }

        public ICollection<EventReservation> EventReservations { get; set; }
    }
}
