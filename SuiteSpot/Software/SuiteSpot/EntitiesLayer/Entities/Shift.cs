using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagement.Entities
{
    [Table("shift")]
    public class Shift
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TimeSpan ShiftStart { get; set; }
        public TimeSpan ShiftEnd { get; set; }

        public ICollection<Schedule> Schedules { get; set; }
    }
}
