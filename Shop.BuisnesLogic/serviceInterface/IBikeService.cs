using Shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.BuisnesLogic.serviceInterface
{
    public interface IBikeService
    {
        IEnumerable<Bike> GetAllBikes();
        Bike GetBikeById(Guid id);
        void AddBike(Bike bike);
        void UpdateBike(Bike bike);
        void DeleteBike(Guid id);

    }
}
