using EntitiesLayer.Entities;
using HotelManagement.Entities;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

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
        public DbSet<RoomReservationGuest> RoomReservationGuests { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Schedule>()
                .ToTable("schedule")
                .HasKey(s => s.Id);

            modelBuilder.Entity<EmployeeSchedule>()
                .ToTable("employee_schedule")
                .HasKey(es => es.Id);

            modelBuilder.Entity<FacilityBilling>()
              .ToTable("facility_billing")
              .HasKey(es => es.Id);

            modelBuilder.Entity<Employee>()
                .ToTable("employee")
                .HasKey(e => e.Id);

            modelBuilder.Entity<Shift>()
                .ToTable("shift")
                .HasKey(s => s.Id);

            modelBuilder.Entity<Schedule>()
                .HasMany(s => s.EmployeeSchedules)
                .WithRequired(es => es.Schedule)
                .HasForeignKey(es => es.ScheduleId);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.EmployeeSchedules)
                .WithRequired(es => es.Employee)
                .HasForeignKey(es => es.EmployeeId);

            modelBuilder.Entity<Schedule>()
                .HasRequired(s => s.Shift)
                .WithMany(sh => sh.Schedules)
                .HasForeignKey(s => s.ShiftId);
        }
    }
}
