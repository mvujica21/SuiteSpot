using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagement.Entities
{
    public class Bill
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }

        [Column("room_reservation_id")]

        public int? RoomReservationId { get; set; }
        [Column("room_reservation_id")]

        public RoomReservation RoomReservation { get; set; }

        public ICollection<FacilityBilling> FacilityBillings { get; set; }
    }
}
