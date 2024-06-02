using DataAccessLayer.Repositories;
using HotelManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

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

        public bool ValidateCredentials(string username, string password)
        {
            using (var employeeRepository = new EmployeeRepository())
            {
                var employee = employeeRepository.GetEmployeeByUsername(username);
                if (employee != null)
                {
                    return VerifyPassword(password, employee.PasswordHash, employee.PasswordSalt);
                }
            }
            return false;
        }

        public bool AddEmployee(string username, string password, string firstName, string lastName, string email, string phoneNumber, int roleId)
        {
            using (var employeeRepository = new EmployeeRepository())
            {
                CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

                var employee = new Employee
                {
                    Username = username,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    PhoneNumber = phoneNumber,
                    RoleId = roleId
                };

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

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPassword(string password, byte[] storedHash, byte[] storedSalt)
        {
            using (var hmac = new HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }
            return true;
        }
    }
}
