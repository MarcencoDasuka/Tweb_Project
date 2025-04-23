using Shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.BuisnesLogic.serviceInterface
{
    public interface IRoleService
    {
        IEnumerable<Role> GetAllRoles();
        Role GetRoleById(Guid id);
        void AddRole(Role role);
        void UpdateRole(Role role);
        void DeleteRole(Guid id);

    }
}
