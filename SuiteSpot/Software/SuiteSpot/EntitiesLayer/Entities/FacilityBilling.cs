namespace HotelManagement.Entities
{
    public class FacilityBilling
    {
        public int Id { get; set; }
        public int BillId { get; set; }
        public Bill Bill { get; set; }

        public int FacilityId { get; set; }
        public Facility Facility { get; set; }

        public int Amount { get; set; }
    }
}
