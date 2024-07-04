using DataAccessLayer.Repositories;
using EntitiesLayer.Entities;
using HotelManagement.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class RoomReservationService
    {
        public async Task<RoomReservation> CreateRoomReservationAsync(Room room, List<Guest> guests, DateTime startDate, DateTime endDate, int employeeId)
        {
            using (var reservationRepository = new RoomReservationRepository())
            {
                var roomReservation = new RoomReservation
                {
                    RoomId = room.Id,
                    StartDate = startDate,
                    EndDate = endDate,
                    Status = "Pending",
                    EmployeeId = employeeId,
                    NumberOfGuests = guests.Count,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                await reservationRepository.AddRoomReservationAsync(roomReservation);

                foreach (var guest in guests)
                {
                    var existingGuest = await reservationRepository.GetGuestByEmailAsync(guest.Email);
                    if (existingGuest != null)
                    {
                        guest.Id = existingGuest.Id;
                    }
                    else
                    {
                        await reservationRepository.AddGuestAsync(guest);
                    }

                    var roomReservationGuest = new RoomReservationGuest
                    {
                        RoomReservationId = roomReservation.Id,
                        GuestId = guest.Id
                    };
                    await reservationRepository.AddRoomReservationGuestAsync(roomReservationGuest);
                }

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
