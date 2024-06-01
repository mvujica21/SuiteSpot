using HotelManagement.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class FaciltiyRepository : IDisposable
    {
        public HotelDbContext Context { get; set; }
        public DbSet<Facility> Facilities { get; set; }
        public FaciltiyRepository()
        {
            Context = new HotelDbContext();
            Facilities = Context.Set<Facility>();
        }
        public IQueryable<Facility> GetFacilities()
        {
            return Context.Facilities.AsQueryable();
        }
        public bool AddFacilities(Facility facility)
        {
            try
            {
                Context.Facilities.Add(facility);
                Context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error adding Facility: {ex.Message}");
                var inner = ex.InnerException;
                while (inner != null)
                {
                    Debug.WriteLine($"Inner Exception: {inner.Message}");
                    inner = inner.InnerException;
                }
                throw;
            }
        }

        public bool UpdateFacility(Facility facility)
        {
            try
            {
                Facility currentFacility = Facilities.Find(facility.Id);
                if (currentFacility != null)
                {
                    Context.Entry(currentFacility).CurrentValues.SetValues(facility);
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

        public bool DeleteFacility(Facility facility)
        {
            try
            {
                Facility currentFacility = Facilities.Find(facility.Id);
                if (currentFacility != null)
                {
                    Facilities.Remove(currentFacility);
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
