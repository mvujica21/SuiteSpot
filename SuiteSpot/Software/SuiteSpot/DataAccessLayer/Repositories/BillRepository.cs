using HotelManagement.Entities;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class BillRepository : IDisposable
    {
        private readonly HotelDbContext _context;

        public BillRepository()
        {
            _context = new HotelDbContext();
        }

        public async Task SaveBillAsync(Bill bill)
        {
            if (_context.Entry(bill).State == EntityState.Detached)
            {
                _context.Bills.Attach(bill);
            }

            foreach (var facilityBilling in bill.FacilityBillings)
            {
                if (_context.Entry(facilityBilling).State == EntityState.Detached)
                {
                    _context.FacilityBillings.Attach(facilityBilling);
                }
            }

            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
