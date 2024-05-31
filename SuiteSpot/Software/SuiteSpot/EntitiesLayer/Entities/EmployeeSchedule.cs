namespace HotelManagement.Entities
{
    public class EmployeeSchedule
    {
        public int Id { get; set; }
        public int ScheduleId { get; set; }
        public Schedule Schedule { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
