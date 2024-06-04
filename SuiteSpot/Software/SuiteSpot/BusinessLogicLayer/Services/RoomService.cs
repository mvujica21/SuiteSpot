using DataAccessLayer.Repositories;
using HotelManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class RoomService
    {
        public List<Room> GetRooms()
        {
            using (var roomsRepository = new RoomRepository())
            {
                return roomsRepository.GetRooms().ToList();
            }
        }
        public bool AddRoom(Room room)
        {
            using (var roomsRepository = new RoomRepository())
            {
                return roomsRepository.AddRoom(room);
            }
        }

        public bool UpdateRoom(Room room)
        {
            using (var roomsRepository = new RoomRepository())
            {
                return roomsRepository.UpdateRoom(room);
            }
        }

        public bool DeleteRoom(Room room)
        {
            using (var roomsRepository = new RoomRepository())
            {
                return roomsRepository.DeleteRoom(room);
            }
        }

        public async Task<List<Room>> GetAvailableRooms(DateTime checkInDate, DateTime checkOutDate, int roomCount, int guestCount)
        {
            using (var roomsRepository = new RoomRepository())
            {
                return await roomsRepository.GetAvailableRooms(checkInDate, checkOutDate, roomCount, guestCount);
            }
        }
    }
}
