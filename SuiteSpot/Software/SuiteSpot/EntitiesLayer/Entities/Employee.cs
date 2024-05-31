using System.Collections.Generic;

namespace HotelManagement.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public int role_id { get; set; }
        public Role Role { get; set; }

        public ICollection<RoomReservation> RoomReservations { get; set; }
        public ICollection<EmployeeSchedule> EmployeeSchedules { get; set; }
    }
}
