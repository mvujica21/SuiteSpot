using BusinessLogicLayer.Services;
using HotelManagement.Entities;
using System.Collections.Generic;
using System.Windows.Controls;

namespace PresentationLayer.Forms
{
    public partial class ucManageWorkSchedule : UserControl
    {
        private ScheduleService scheduleService;

        public ucManageWorkSchedule()
        {
            InitializeComponent();
            scheduleService = new ScheduleService();
            LoadSchedules();
        }

        private void LoadSchedules()
        {
            var schedules = scheduleService.GetSchedulesForNext7Days();
            var scheduleDisplayList = new List<ScheduleDisplay>();

            foreach (var schedule in schedules)
            {
                foreach (var employeeSchedule in schedule.EmployeeSchedules)
                {
                    scheduleDisplayList.Add(new ScheduleDisplay
                    {
                        Date = schedule.Date.ToString("yyyy-MM-dd"),
                        EmployeeName = $"{employeeSchedule.Employee.FirstName} {employeeSchedule.Employee.LastName}",
                        Shift = $"{schedule.ShiftStart} - {schedule.ShiftEnd}"
                    });
                }
            }

            dgWorkSchedule.ItemsSource = scheduleDisplayList;
        }
    }

    public class ScheduleDisplay
    {
        public string Date { get; set; }
        public string EmployeeName { get; set; }
        public string Shift { get; set; }
    }
}
