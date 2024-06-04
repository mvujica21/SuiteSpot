using DataAccessLayer.Repositories;
using HotelManagement.Entities;
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
    }
}
