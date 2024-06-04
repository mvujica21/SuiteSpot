using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagement.Entities
{
    public class FacilityBilling
    {
        public int Id { get; set; }
        public int BillId { get; set; }
        [Column("bill_id")]

        public Bill Bill { get; set; }
        public int FacilityId { get; set; }
        [Column("facility_id")]

        public Facility Facility { get; set; }

        public int Amount { get; set; }
    }
}
