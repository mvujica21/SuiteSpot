using System.Collections.Generic;
using System;

namespace HotelManagement.Entities
{
    public class Schedule
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public DateTime ShiftStart { get; set; }
        public DateTime ShiftEnd { get; set; }

        public int ShiftId { get; set; }
        public Shift Shift { get; set; }

        public ICollection<EmployeeSchedule> EmployeeSchedules { get; set; }
    }
}
