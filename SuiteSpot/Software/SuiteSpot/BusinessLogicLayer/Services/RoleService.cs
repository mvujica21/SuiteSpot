using DataAccessLayer.Repositories;
using HotelManagement.Entities;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogicLayer.Services
{
    public class RoleService
    {
        public List<Role> GetRoles()
        {
            using (var roleRepository = new RoleRepository())
            {
                return roleRepository.GetRoles().ToList();
            }
        }
        public bool AddRole(Role role)
        {
            using (var roleRepository = new RoleRepository())
            {
                return roleRepository.AddRole(role);
            }
        }

        public bool UpdateRole(Role role)
        {
            using (var roleRepository = new RoleRepository())
            {
                return roleRepository.UpdateRole(role);
            }
        }

        public bool DeleteRole(Role role)
        {
            using (var roleRepository = new RoleRepository())
            {
                return roleRepository.DeleteRole(role);
            }
        }
    }
}
