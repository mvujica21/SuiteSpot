using HotelManagement.Entities;
using System;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;

namespace DataAccessLayer.Repositories
{
    public class RoleRepository : IDisposable
    {
        public HotelDbContext Context { get; set; }
        public DbSet<Role> Roles { get; set; }
        public RoleRepository()
        {
            Context = new HotelDbContext();
            Roles = Context.Set<Role>();
        }
        public IQueryable<Role> GetRoles()
        {
            return Context.Roles.AsQueryable();
        }
        public bool AddRole(Role role)
        {
            try
            {
                Context.Roles.Add(role);
                Context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error adding role: {ex.Message}");
                var inner = ex.InnerException;
                while (inner != null)
                {
                    Debug.WriteLine($"Inner Exception: {inner.Message}");
                    inner = inner.InnerException;
                }
                throw;
            }
        }

        public bool UpdateRole(Role role)
        {
            try
            {
                Role currentRole = Roles.Find(role.Id);
                if (currentRole != null)
                {
                    Context.Entry(currentRole).CurrentValues.SetValues(role);
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

        public bool DeleteRole(Role role)
        {
            try
            {
                Role currentRole = Roles.Find(role.Id);
                if (currentRole != null)
                {
                    Roles.Remove(currentRole);
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
