using System.Collections.Generic;

namespace HotelManagement.Entities
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
}
