using HotelManagement.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer.Entities
{
    [Table("room_reservation_guest")]

    public class RoomReservationGuest
    {
        public int Id { get; set; }
        [Column("room_reservation_id")]

        public int RoomReservationId { get; set; }
        public RoomReservation RoomReservation { get; set; }

        [Column("guest_id")]

        public int GuestId { get; set; }
        public Guest Guest { get; set; }
    }
}
