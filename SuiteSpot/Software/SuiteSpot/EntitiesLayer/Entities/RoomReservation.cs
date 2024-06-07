using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
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

        [Column("number_of_guests")]
        public int NumberOfGuests { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public ICollection<Bill> Bills { get; set; }
        public ICollection<RoomReservationGuest> RoomReservationGuests { get; set; }


        [NotMapped]
        public List<Guest> Guests { get; set; } = new List<Guest>();
    }
}
