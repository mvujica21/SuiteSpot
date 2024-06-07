using System.Collections.Generic;

namespace EntitiesLayer.ViewModel
{
    public class EmployeeScheduleViewModel
    {
        public int EmployeeId { get; set; }

        public string EmployeeName { get; set; }
        public Dictionary<string, string> Shifts { get; set; }

        public EmployeeScheduleViewModel()
        {
            Shifts = new Dictionary<string, string>();
        }
    }

}
