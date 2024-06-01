using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagement.Entities
{
    [Table("schedule")]
    public class Schedule
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan ShiftStart { get; set; }
        public TimeSpan ShiftEnd { get; set; }
        [Column("shift_id")]
        public int ShiftId { get; set; }

        [ForeignKey("shift_id")]
        public Shift Shift { get; set; }

        public ICollection<EmployeeSchedule> EmployeeSchedules { get; set; }
    }
}
