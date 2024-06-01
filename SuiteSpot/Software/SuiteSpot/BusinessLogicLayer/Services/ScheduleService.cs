using DataAccessLayer.Repositories;
using HotelManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogicLayer.Services
{
    public class ScheduleService
    {
        public List<Schedule> GetSchedulesForNext7Days()
        {
            using (var scheduleRepository = new ScheduleRepository())
            {
                var startDate = DateTime.Today;
                var endDate = startDate.AddDays(7);
                return scheduleRepository.GetSchedules(startDate, endDate).ToList();
            }
        }
    }
}
