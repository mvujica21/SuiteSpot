using HotelManagement.Entities;
using System;
using System.Collections.Generic;
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

        public async Task<List<Bill>> GetActiveBillsAsync()
        {
            return await _context.Bills.Where(b => b.Status == "Active").ToListAsync();
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

        public async Task<List<FacilityBilling>> GetFacilityBillingsByBillIdAsync(int billId)
        {
            return await _context.FacilityBillings.Where(fb => fb.BillId == billId).Include(fb => fb.Facility).ToListAsync();
        }

        public async Task<FacilityBilling> GetFacilityBillingAsync(int billId, int facilityId)
        {
            return await _context.FacilityBillings.FirstOrDefaultAsync(fb => fb.BillId == billId && fb.FacilityId == facilityId);
        }

        public async Task AddFacilityBillingAsync(FacilityBilling facilityBilling)
        {
            _context.FacilityBillings.Add(facilityBilling);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateFacilityBillingAsync(FacilityBilling facilityBilling)
        {
            _context.Entry(facilityBilling).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
