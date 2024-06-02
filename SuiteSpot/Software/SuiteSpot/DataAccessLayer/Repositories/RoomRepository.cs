using HotelManagement.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
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
    }
}
