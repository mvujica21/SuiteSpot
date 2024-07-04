using System.Collections.Generic;

namespace HotelManagement.Entities
{
    public class Facility
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public ICollection<FacilityBilling> FacilityBillings { get; set; }
    }
}
