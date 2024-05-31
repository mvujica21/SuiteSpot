using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagement.Entities
{
    [Table("room")]

    public class Room
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string Type { get; set; }
        public int MaxCapacity { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Number { get; set; }
        public bool Reserved { get; set; }

        public ICollection<Review> Reviews { get; set; }
        public ICollection<RoomReservation> RoomReservations { get; set; }
    }
}

