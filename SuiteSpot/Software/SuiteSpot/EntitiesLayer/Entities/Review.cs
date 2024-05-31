using System;

namespace HotelManagement.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public float Rating { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }

        public int GuestId { get; set; }
        public Guest Guest { get; set; }

        public int RoomId { get; set; }
        public Room Room { get; set; }
    }
}
