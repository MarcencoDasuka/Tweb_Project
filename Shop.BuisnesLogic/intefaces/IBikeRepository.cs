using Shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.BuisnesLogic.intefaces
{
    public interface IBikeRepository
    {
        IEnumerable<Bike> GetAll();
        Bike GetById(Guid id);
        void Add(Bike bike);
        void Update(Bike bike);
        void Delete(Guid id);

    }
}
