using System.Collections.Generic;
using System;

namespace HotelManagement.Entities
{
    public class Shift
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ShiftStart { get; set; }
        public DateTime ShiftEnd { get; set; }

        public ICollection<Schedule> Schedules { get; set; }
    }
}
