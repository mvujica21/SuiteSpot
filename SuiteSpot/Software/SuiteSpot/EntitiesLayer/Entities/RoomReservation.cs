using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagement.Entities
{
    [Table("room_reservation")]

    public class RoomReservation
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }

        [Column("room_id")]

        public int RoomId { get; set; }
        public Room Room { get; set; }
        [Column("employee_id")]

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        [Column("guest_id")]

        public int GuestId { get; set; }
        public Guest Guest { get; set; }

        [Column("number_of_guests")]

        public int NumberOfGuests { get; set; }

        public ICollection<Bill> Bills { get; set; }
        [NotMapped]
        public List<Guest> Guests { get; set; } = new List<Guest>();
    }
}
