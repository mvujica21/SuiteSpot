using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagement.Entities
{
    [Table("facility_billing")]

    public class FacilityBilling
    {
        public int Id { get; set; }
        [Column("bill_id")]

        public int BillId { get; set; }

        public Bill Bill { get; set; }
        [Column("facility_id")]

        public int FacilityId { get; set; }

        public Facility Facility { get; set; }

        public int Amount { get; set; }
    }
}
