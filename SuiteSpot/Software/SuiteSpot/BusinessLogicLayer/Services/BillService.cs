using DataAccessLayer.Repositories;
using HotelManagement.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class BillService
    {
        public async Task SaveBillAsync(Bill bill)
        {
            using (var billRepository = new BillRepository())
            {
                await billRepository.SaveBillAsync(bill);
            }
        }
        public async Task<Bill> GetOrCreateBillAsync(int roomReservationId)
        {
            using (var billRepository = new BillRepository())
            {
                var bill = await billRepository.GetBillByRoomReservationIdAsync(roomReservationId);
                if (bill == null)
                {
                    bill = new Bill
                    {
                        RoomReservationId = roomReservationId,
                        FacilityBillings = new List<FacilityBilling>()
                    };
                    await billRepository.AddBillAsync(bill);
                }
                return bill;
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
