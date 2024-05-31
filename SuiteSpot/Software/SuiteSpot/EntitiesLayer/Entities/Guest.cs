using System.Collections.Generic;

namespace HotelManagement.Entities
{
    public class Guest
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public ICollection<Review> Reviews { get; set; }
        public ICollection<RoomReservation> RoomReservations { get; set; }
        public ICollection<EventReservation> EventReservations { get; set; }
    }
}
