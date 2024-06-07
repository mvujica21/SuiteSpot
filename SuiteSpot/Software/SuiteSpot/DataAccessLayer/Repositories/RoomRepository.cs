using HotelManagement.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class RoomRepository : IDisposable
    {
        public HotelDbContext Context { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public RoomRepository()
        {
            Context = new HotelDbContext();
            Rooms = Context.Set<Room>();
        }
        public IQueryable<Room> GetRooms()
        {
            return Context.Room.AsQueryable();
        }
        public Room GetRoomById(int id)
        {
            return Rooms.Find(id);
        }
        public List<Room> GetRoomsByCapacity(int numberOfGuests)
        {
            return Rooms.Where(r => r.maxxCapacity >= numberOfGuests).ToList();
        }
        public bool AddRoom(Room room)
        {
            try
            {
                Context.Room.Add(room);
                Context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error adding room: {ex.Message}");
                var inner = ex.InnerException;
                while (inner != null)
                {
                    Debug.WriteLine($"Inner Exception: {inner.Message}");
                    inner = inner.InnerException;
                }
                throw;
            }
        }

        public bool UpdateRoom(Room room)
        {
            try
            {
                Room currentRoom = Rooms.Find(room.Id);
                if (currentRoom != null)
                {
                    Context.Entry(currentRoom).CurrentValues.SetValues(room);
                    Context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public bool DeleteRoom(Room room)
        {
            try
            {
                Room currentRoom = Rooms.Find(room.Id);
                if (currentRoom != null)
                {
                    Rooms.Remove(currentRoom);
                    Context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
        public void Dispose()
        {
            Context.Dispose();
        }

        public async Task<List<Room>> GetAvailableRooms(DateTime checkInDate, DateTime checkOutDate, int roomCount, int guestCount)
        {
            var availableRooms = await Rooms
                .Where(r => r.maxxCapacity >= guestCount)
                .ToListAsync();

            var bookedRooms = await Context.RoomReservations
                .Where(b => (b.StartDate >= checkInDate && b.EndDate <= checkOutDate) ||
                            (b.StartDate >= checkInDate && b.EndDate <= checkOutDate) ||
                            (b.StartDate <= checkInDate && b.EndDate >= checkOutDate))
                .Select(b => b.RoomId)
                .ToListAsync();

            var availableRoomsIds = availableRooms
                .Where(r => !bookedRooms.Contains(r.Id))
                .Select(r => r.Id)
                .ToList();

            var availableRoomsList = await Rooms
                .Where(r => availableRoomsIds.Contains(r.Id))
                .ToListAsync();

            return availableRoomsList;
        }
    }
}
