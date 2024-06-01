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

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
