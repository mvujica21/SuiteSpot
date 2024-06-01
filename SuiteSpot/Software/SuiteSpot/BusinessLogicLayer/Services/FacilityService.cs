using DataAccessLayer.Repositories;
using HotelManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class FacilityService
    {
        public List<Facility> GetFacilites()
        {
            using (var facilityRepository = new FaciltiyRepository())
            {
                return facilityRepository.GetFacilities().ToList();
            }
        }
        public bool AddFacility(Facility facility)
        {
            using (var facilityRepository = new FaciltiyRepository())
            {
                return facilityRepository.AddFacilities(facility);
            }
        }

        public bool UpdateFacility(Facility facility)
        {
            using (var facilityRepository = new FaciltiyRepository())
            {
                return facilityRepository.UpdateFacility(facility);
            }
        }

        public bool DeleteFacility(Facility facility)
        {
            using (var facilityRepository = new FaciltiyRepository())
            {
                return facilityRepository.DeleteFacility(facility);
            }
        }
    }
}
