using System.Collections.Generic;
using System;

namespace HotelManagement.Entities
{
    public class RoomReservation
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }

        public int RoomId { get; set; }
        public Room Room { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public int GuestId { get; set; }
        public Guest Guest { get; set; }

        public int NumberOfGuests { get; set; }

        public ICollection<Bill> Bills { get; set; }
    }
}
