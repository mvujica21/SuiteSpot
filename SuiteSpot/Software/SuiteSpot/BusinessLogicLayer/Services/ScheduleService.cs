using DataAccessLayer.Repositories;
using EntitiesLayer.ViewModel;
using HotelManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogicLayer.Services
{
    public class ScheduleService
    {
        public List<EmployeeScheduleViewModel> GetTransformedSchedulesForNext7Days()
        {
            using (var scheduleRepository = new ScheduleRepository())
            {
                var startDate = DateTime.Today;
                var endDate = startDate.AddDays(7);
                var schedules = scheduleRepository.GetSchedules(startDate, endDate);

                var employeeSchedules = new Dictionary<string, EmployeeScheduleViewModel>();

                foreach (var schedule in schedules)
                {
                    foreach (var employeeSchedule in schedule.EmployeeSchedules)
                    {
                        var employeeId = employeeSchedule.EmployeeId;
                        var employeeName = $"{employeeSchedule.Employee.FirstName} {employeeSchedule.Employee.LastName}";
                        if (!employeeSchedules.ContainsKey(employeeName))
                        {
                            employeeSchedules[employeeName] = new EmployeeScheduleViewModel
                            {
                                EmployeeId = employeeId,
                                EmployeeName = employeeName
                            };
                        }

                        var dateKey = schedule.Date.ToString("yyyy-MM-dd");
                        var shiftStart = schedule.ShiftStart.ToString(@"hh\:mm");
                        var shiftEnd = schedule.ShiftEnd.ToString(@"hh\:mm");
                        var shiftTime = $"{shiftStart} - {shiftEnd}";
                        employeeSchedules[employeeName].Shifts[dateKey] = shiftTime;
                    }
                }

                return employeeSchedules.Values.ToList();
            }
        }

        public void AddSchedule(DateTime date, TimeSpan shiftStart, TimeSpan shiftEnd, int shiftId, int employeeId)
        {
            using (var scheduleRepository = new ScheduleRepository())
            {
                var schedule = new Schedule
                {
                    Date = date,
                    ShiftStart = shiftStart,
                    ShiftEnd = shiftEnd,
                    ShiftId = shiftId,
                    EmployeeSchedules = new List<EmployeeSchedule>()
                };

                scheduleRepository.AddSchedule(schedule, employeeId);
            }
        }
        public void DeleteSchedule(DateTime date, int employeeId)
        {
            using (var scheduleRepository = new ScheduleRepository())
            {
                scheduleRepository.DeleteSchedule(date, employeeId);
            }
        }
    }
}
