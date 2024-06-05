using DataAccessLayer.Repositories;
using HotelManagement.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class RoomReservationService
    {
        public async Task<RoomReservation> CreateRoomReservationAsync(Room room, Guest guest, DateTime startDate, DateTime endDate, int employeeId)
        {
            using (var reservationRepository = new RoomReservationRepository())
            {
                var existingGuest = await reservationRepository.GetGuestByEmailAsync(guest.Email);
                if (existingGuest != null)
                {
                    guest = existingGuest;
                }
                else
                {
                    await reservationRepository.AddGuestAsync(guest);
                }
                reservationRepository.AttachRoom(room);

                var roomReservation = new RoomReservation
                {
                    RoomId = room.Id,
                    GuestId = guest.Id,
                    EmployeeId = employeeId,
                    StartDate = startDate,
                    EndDate = endDate,
                    Status = "Pending",
                    NumberOfGuests = 1 
                };

                await reservationRepository.AddRoomReservationAsync(roomReservation);
                return roomReservation;
            }
        }

        public async Task<RoomReservation> GetRoomReservationAsync(int id)
        {
            using (var reservationRepository = new RoomReservationRepository())
            {
                return await reservationRepository.GetRoomReservationAsync(id);
            }
        }

        public async Task<List<RoomReservation>> GetUnfinishedRoomReservationsAsync()
        {
            using (var reservationRepository = new RoomReservationRepository())
            {
                return await reservationRepository.GetUnfinishedRoomReservationsAsync();
            }
        }

        public async Task UpdateRoomReservationAsync(RoomReservation roomReservation)
        {
            using (var reservationRepository = new RoomReservationRepository())
            {
                await reservationRepository.UpdateRoomReservationAsync(roomReservation);
            }
        }
    }
}
