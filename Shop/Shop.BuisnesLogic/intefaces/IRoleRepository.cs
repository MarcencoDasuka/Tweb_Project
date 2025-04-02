using Shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.BuisnesLogic.intefaces
{
    public interface IRoleRepository
    {
        IEnumerable<Role> GetAll();
        Role GetById(Guid id);
        void Add(Role role);
        void Update(Role role);
        void Delete(Guid id);
    }
}
