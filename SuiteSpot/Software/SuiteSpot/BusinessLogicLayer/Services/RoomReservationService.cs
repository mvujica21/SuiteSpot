using DataAccessLayer.Repositories;
using HotelManagement.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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
                    NumberOfGuests = 1 // Adjust as necessary
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
    }
}
