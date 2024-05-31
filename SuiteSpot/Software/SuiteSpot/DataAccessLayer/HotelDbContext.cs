using HotelManagement.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class HotelDbContext : DbContext
    {
        public HotelDbContext() : base("name=HotelDbContext")
        {
        }
        public DbSet<Room> Room { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<RoomReservation> RoomReservations { get; set; }
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<EmployeeSchedule> EmployeeSchedules { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<FacilityBilling> FacilityBillings { get; set; }
        public DbSet<ContactDetails> ContactDetails { get; set; }
        public DbSet<EventSpace> EventSpaces { get; set; }
        public DbSet<EventReservation> EventReservations { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.PluralizingTableNameConvention>();
        }
    }
}
