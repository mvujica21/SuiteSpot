using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagement.Entities
{
    [Table("employee_schedule")]
    public class EmployeeSchedule
    {
        public int Id { get; set; }

        [Column("schedule_id")]
        public int ScheduleId { get; set; }
        public Schedule Schedule { get; set; }

        [Column("employee_id")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
