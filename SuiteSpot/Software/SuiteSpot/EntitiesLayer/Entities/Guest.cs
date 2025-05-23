﻿using EntitiesLayer.Entities;
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

        public ICollection<RoomReservationGuest> RoomReservationGuests { get; set; }
    }
}
