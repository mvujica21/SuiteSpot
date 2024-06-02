using HotelManagement.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataAccessLayer.Repositories
{
    public class ScheduleRepository : IDisposable
    {
        public HotelDbContext Context { get; set; }
        public DbSet<Schedule> Schedules { get; set; }

        public ScheduleRepository()
        {
            Context = new HotelDbContext();
            Schedules = Context.Set<Schedule>();
        }

        public IEnumerable<Schedule> GetSchedules(DateTime startDate, DateTime endDate)
        {
            return Schedules
                .Include(s => s.Shift)
                .Include(s => s.EmployeeSchedules.Select(es => es.Employee))
                .Where(s => s.Date >= startDate && s.Date <= endDate)
                .ToList();
        }

        public void AddSchedule(Schedule schedule, int employeeId)
        {
            var employeeSchedule = new EmployeeSchedule
            {
                EmployeeId = employeeId,
                Schedule = schedule
            };

            schedule.EmployeeSchedules.Add(employeeSchedule);
            Schedules.Add(schedule);
            Context.SaveChanges();
        }
        public void DeleteSchedule(DateTime date, int employeeId)
        {
            var employeeSchedule = Context.Set<EmployeeSchedule>()
                .Include(es => es.Schedule)
                .FirstOrDefault(es => es.Schedule.Date == date && es.EmployeeId == employeeId);

            if (employeeSchedule != null)
            {
                Context.Set<EmployeeSchedule>().Remove(employeeSchedule);
                Context.SaveChanges();
            }
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
