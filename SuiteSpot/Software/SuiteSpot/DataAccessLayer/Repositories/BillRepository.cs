using HotelManagement.Entities;
using System;
using System.Data.Entity;
using System.Linq;
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
        public async Task<Bill> GetBillByRoomReservationIdAsync(int roomReservationId)
        {
            return await _context.Bills
                .Include(b => b.FacilityBillings.Select(fb => fb.Facility))
                .FirstOrDefaultAsync(b => b.RoomReservationId == roomReservationId);
        }

        public async Task AddBillAsync(Bill bill)
        {
            _context.Bills.Add(bill);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBillAsync(Bill bill)
        {
            _context.Entry(bill).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
