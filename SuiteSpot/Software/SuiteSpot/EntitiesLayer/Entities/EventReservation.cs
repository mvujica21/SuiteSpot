using System;

namespace HotelManagement.Entities
{
    public class EventReservation
    {
        public int Id { get; set; }
        public string EventName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public int NumberOfAttendees { get; set; }
        public string Status { get; set; }

        public int GuestId { get; set; }
        public Guest Guest { get; set; }

        public int ContactDetailsId { get; set; }
        public ContactDetails ContactDetails { get; set; }

        public int EventSpaceId { get; set; }
        public EventSpace EventSpace { get; set; }
    }
}
