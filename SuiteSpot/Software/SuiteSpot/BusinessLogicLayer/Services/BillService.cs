using DataAccessLayer.Repositories;
using HotelManagement.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class BillService
    {
        public async Task<List<Bill>> GetActiveBillsAsync()
        {
            using (var billRepository = new BillRepository())
            {
                return await billRepository.GetActiveBillsAsync();
            }
        }

        public async Task<Bill> CreateBillAsync()
        {
            using (var billRepository = new BillRepository())
            {
                var bill = new Bill
                {
                    Date = DateTime.Now,
                    Price = 0,
                    FacilityBillings = new List<FacilityBilling>(),
                    Status = "Active"
                };
                await billRepository.AddBillAsync(bill);
                return bill;
            }
        }
        public async Task<List<FacilityBilling>> GetFacilityBillingsByBillIdAsync(int billId)
        {
            using (var billRepository = new BillRepository())
            {
                return await billRepository.GetFacilityBillingsByBillIdAsync(billId);
            }
        }
        public async Task SaveFacilityBillingAsync(FacilityBilling facilityBilling)
        {
            using (var billRepository = new BillRepository())
            {
                await billRepository.SaveFacilityBillingAsync(facilityBilling);
            }
        }


        public async Task AssignRoomReservationAsync(Bill bill, int roomReservationId)
        {
            using (var billRepository = new BillRepository())
            {
                // Check if the room reservation exists before assigning it
                var reservationRepository = new RoomReservationRepository();
                var reservation = await reservationRepository.GetRoomReservationAsync(roomReservationId);

                if (reservation == null)
                {
                    throw new ArgumentException("Invalid room reservation ID");
                }

                bill.RoomReservationId = roomReservationId;
                await billRepository.UpdateBillAsync(bill);
            }
        }

        public async Task FinalizeBillAsync(Bill bill)
        {
            using (var billRepository = new BillRepository())
            {
                await billRepository.UpdateBillAsync(bill);
            }
        }
    }
}
