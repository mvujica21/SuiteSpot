using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagement.Entities
{
    [Table("employee")]
    public class Employee
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        [Column("role_id")]
        public int RoleId { get; set; }

        [ForeignKey("RoleId")]
        public Role Role { get; set; }

        public ICollection<EmployeeSchedule> EmployeeSchedules { get; set; }
        public string FullName => $"{FirstName} {LastName}";

    }
}
