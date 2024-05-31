using DataAccessLayer.Repositories;
using HotelManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class EmployeeService
    {
        public EmployeeService() { }
        public List<Employee> GetEmployees()
        {
            using (var employeeRepository = new EmployeeRepository())
            {
                return employeeRepository.GetEmployees().ToList();
            }
        }

        public bool AddEmployee(Employee employee)
        {
            using (var employeeRepository = new EmployeeRepository())
            {
                return employeeRepository.AddEmployee(employee);
            }
        }

        public bool UpdateEmployee(Employee employee)
        {
            using (var employeeRepository = new EmployeeRepository())
            {
                return employeeRepository.UpdateEmployee(employee);
            }
        }

        public bool DeleteEmployee(Employee employee)
        {
            using (var employeeRepository = new EmployeeRepository())
            {
                return employeeRepository.DeleteEmployee(employee);
            }
        }
    }
}
