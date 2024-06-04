using HotelManagement.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class RoomReservationRepository : IDisposable
    {
        public HotelDbContext Context { get; set; }
        public DbSet<RoomReservation> RoomReservations { get; set; }
        public DbSet<Guest> Guests { get; set; }

        public RoomReservationRepository()
        {
            Context = new HotelDbContext();
            RoomReservations = Context.Set<RoomReservation>();
            Guests = Context.Set<Guest>();
        }

        public IQueryable<RoomReservation> GetRoomReservations()
        {
            return RoomReservations.Include(r => r.Room).Include(r => r.Guest).AsQueryable();
        }

        public async Task AddRoomReservationAsync(RoomReservation reservation)
        {
            try
            {
                RoomReservations.Add(reservation);
                await Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error adding room reservation: {ex.Message}");
                throw;
            }
        }

        public async Task<Guest> AddGuestAsync(Guest guest)
        {
            try
            {
                Guests.Add(guest);
                await Context.SaveChangesAsync();
                return guest;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error adding guest: {ex.Message}");
                throw;
            }
        }

        public async Task<RoomReservation> GetRoomReservationAsync(int id)
        {
            return await RoomReservations.Include(r => r.Guest).Include(r => r.Room).FirstOrDefaultAsync(r => r.Id == id);
        }

        public void Dispose()
        {
            Context.Dispose();
        }

        public async Task<Guest> GetGuestByEmailAsync(string email)
        {
            return await Context.Guests.FirstOrDefaultAsync(g => g.Email == email);
        }

        public void AttachRoom(Room room)
        {
            if (Context.Entry(room).State == EntityState.Detached)
            {
                Context.Room.Attach(room);
            }
        }

        public async Task<List<RoomReservation>> GetUnfinishedRoomReservationsAsync()
        {
            var roomReservations = await RoomReservations
                .Include(r => r.Room)
                .Include(r => r.Guest)
                .Where(r => r.Status != "Finished")
                .ToListAsync();

            var groupedReservations = roomReservations
                .GroupBy(r => new { r.RoomId, r.StartDate, r.EndDate, r.Status })
                .Select(g => new RoomReservation
                {
                    Id = g.First().Id,
                    RoomId = g.Key.RoomId,
                    GuestId = g.First().GuestId, // Select one of the guests for reference
                    StartDate = g.Key.StartDate,
                    EndDate = g.Key.EndDate,
                    Status = g.Key.Status,
                    Room = g.First().Room,
                    Guest = g.First().Guest, // Select one of the guests for reference
                    NumberOfGuests = g.Count(), // Count the number of grouped reservations
                    Guests = g.Select(r => r.Guest).ToList() // Collect all guests in the group
                }).ToList();

            return groupedReservations;
        }
        public async Task UpdateRoomReservationAsync(RoomReservation reservation)
        {
            try
            {
                Context.Entry(reservation).State = EntityState.Modified;
                await Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error updating room reservation: {ex.Message}");
                throw;
            }
        }
        }
    }
