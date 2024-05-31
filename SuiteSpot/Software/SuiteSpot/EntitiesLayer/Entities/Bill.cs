using System.Collections.Generic;
using System;

namespace HotelManagement.Entities
{
    public class Bill
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Price { get; set; }
        public string PaymentType { get; set; }

        public int RoomReservationId { get; set; }
        public RoomReservation RoomReservation { get; set; }

        public ICollection<FacilityBilling> FacilityBillings { get; set; }
    }
}
