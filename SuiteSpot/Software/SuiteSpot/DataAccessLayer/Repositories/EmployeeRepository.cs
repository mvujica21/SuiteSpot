using HotelManagement.Entities;
using System;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;

namespace DataAccessLayer.Repositories
{
    public class EmployeeRepository : IDisposable
    {
        public HotelDbContext Context { get; set; }
        public DbSet<Employee> Employees { get; set; }

        public EmployeeRepository()
        {
            Context = new HotelDbContext();
            Employees = Context.Set<Employee>();
        }
        public IQueryable<Employee> GetEmployees()
        {
            return Employees.AsQueryable();
        }
        public bool AddEmployee(Employee employee)
        {
            try
            {
                Context.Employees.Add(employee);
                Context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error adding employee: {ex.Message}");
                var inner = ex.InnerException;
                while (inner != null)
                {
                    Debug.WriteLine($"Inner Exception: {inner.Message}");
                    inner = inner.InnerException;
                }
                throw;
            }
        }

        public bool UpdateEmployee(Employee employee)
        {
            try
            {
                Employee currentEmployee = Employees.Find(employee.Id);
                if (currentEmployee != null)
                {
                    Context.Entry(currentEmployee).CurrentValues.SetValues(employee);
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

        public bool DeleteEmployee(Employee employee)
        {
            try
            {
                Employee currentEmployee = Employees.Find(employee.Id);
                if (currentEmployee != null)
                {
                    Employees.Remove(currentEmployee);
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
        public Employee GetEmployeeByUsername(string username)
        {
            return Context.Employees.Include(e => e.Role).FirstOrDefault(e => e.Username == username);
        }
        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
