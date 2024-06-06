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
                    Status = "Active",
                    FacilityBillings = new List<FacilityBilling>()
                };
                await billRepository.AddBillAsync(bill);
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
                var existingFacilityBilling = await billRepository.GetFacilityBillingAsync(facilityBilling.BillId, facilityBilling.FacilityId);
                if (existingFacilityBilling != null)
                {
                    existingFacilityBilling.Amount += facilityBilling.Amount;
                    await billRepository.UpdateFacilityBillingAsync(existingFacilityBilling);
                }
                else
                {
                    await billRepository.AddFacilityBillingAsync(facilityBilling);
                }
            }
        }
    }
}
