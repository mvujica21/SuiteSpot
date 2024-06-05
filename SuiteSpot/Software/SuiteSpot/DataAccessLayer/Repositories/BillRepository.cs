using HotelManagement.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
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

        public async Task<Bill> GetBillByRoomReservationIdAsync(int roomReservationId)
        {
            return await _context.Bills.FirstOrDefaultAsync(b => b.RoomReservationId == roomReservationId);
        }
        public async Task<List<FacilityBilling>> GetFacilityBillingsByBillIdAsync(int billId)
        {
            return await _context.FacilityBillings
                .Include(fb => fb.Facility)
                .Where(fb => fb.BillId == billId)
                .ToListAsync();
        }
        public async Task SaveFacilityBillingAsync(FacilityBilling facilityBilling)
        {
            _context.FacilityBillings.Add(facilityBilling);
            await _context.SaveChangesAsync();
        }


        public async Task AddBillAsync(Bill bill)
        {
            _context.Bills.Add(bill);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBillAsync(Bill bill)
        {
            _context.Entry(bill).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var entry = ex.Entries.Single();
                var clientValues = (Bill)entry.Entity;

                var databaseEntry = await entry.GetDatabaseValuesAsync();
                if (databaseEntry == null)
                {
                    // The entity was deleted by another user
                    throw new DbUpdateConcurrencyException("The entity has been deleted by another user.");
                }

                var databaseValues = (Bill)databaseEntry.ToObject();

                // Refresh the original values with the values from the database
                entry.OriginalValues.SetValues(databaseValues);

                // Retry the operation
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteBillAsync(Bill bill)
        {
            _context.Bills.Remove(bill);
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
