using HotelManagement.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

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
                Employees.Add(employee);
                Context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
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
        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
