using EntitiesLayer.Entities;
using HotelManagement.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class RoomReservationRepository : IDisposable
    {
        private readonly HotelDbContext _context;

        public RoomReservationRepository()
        {
            _context = new HotelDbContext();
        }

        public async Task<List<RoomReservation>> GetUnfinishedRoomReservationsAsync()
        {
            return await _context.RoomReservations
                .Include(r => r.Room)
                .Include(r => r.Employee)
                .Include(r => r.RoomReservationGuests.Select(g => g.Guest))
                .Where(r => r.Status != "Finished")
                .ToListAsync();
        }

        public async Task<RoomReservation> GetRoomReservationAsync(int id)
        {
            return await _context.RoomReservations
                .Include(r => r.Room)
                .Include(r => r.Employee)
                .Include(r => r.RoomReservationGuests.Select(g => g.Guest))
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task AddRoomReservationAsync(RoomReservation reservation)
        {
            _context.RoomReservations.Add(reservation);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRoomReservationAsync(RoomReservation reservation)
        {
            _context.Entry(reservation).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<Guest> GetGuestByEmailAsync(string email)
        {
            return await _context.Guests.FirstOrDefaultAsync(g => g.Email == email);
        }

        public async Task AddGuestAsync(Guest guest)
        {
            _context.Guests.Add(guest);
            await _context.SaveChangesAsync();
        }

        public async Task AddRoomReservationGuestAsync(RoomReservationGuest roomReservationGuest)
        {
            _context.RoomReservationGuests.Add(roomReservationGuest);
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
