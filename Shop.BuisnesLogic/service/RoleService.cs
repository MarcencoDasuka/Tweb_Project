using Shop.BuisnesLogic.context;
using Shop.BuisnesLogic.intefaces;
using Shop.BuisnesLogic.repository;
using Shop.BuisnesLogic.serviceInterface;
using Shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.BuisnesLogic.service
{
    public class RoleService : IRoleService
    {
        private readonly RoleRepository _roleRepository;

        public RoleService()
        {
            _roleRepository = new RoleRepository(new AppDbContext());
        }


        public IEnumerable<Role> GetAllRoles()
        {
            return _roleRepository.GetAll();
        }

        public Role GetRoleById(Guid id)
        {
            return _roleRepository.GetById(id);
        }

        public void AddRole(Role role)
        {
            // Логика валидации или проверки
            if (string.IsNullOrWhiteSpace(role.Name))
            {
                throw new ArgumentException("Имя роли не может быть пустым.");
            }
            _roleRepository.Add(role);
        }

        public void UpdateRole(Role role)
        {
            _roleRepository.Update(role);
        }

        public void DeleteRole(Guid id)
        {
            _roleRepository.Delete(id);
        }

    }
}
