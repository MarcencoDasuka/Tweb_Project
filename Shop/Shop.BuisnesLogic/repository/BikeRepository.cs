using Shop.BuisnesLogic.context;
using Shop.BuisnesLogic.intefaces;
using Shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.BuisnesLogic.repository
{
    public class BikeRepository : IBikeRepository
    {
        private readonly AppDbContext _context;

        public BikeRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Bike> GetAll()
        {
            return _context.Bikes.ToList();
        }

        public Bike GetById(Guid id)
        {
            return _context.Bikes.Find(id);
        }

        public void Add(Bike bike)
        {
            _context.Bikes.Add(bike);
            _context.SaveChanges();
        }

        public void Update(Bike bike)
        {
            _context.Entry(bike).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var bike = _context.Bikes.Find(id);
            if (bike != null)
            {
                _context.Bikes.Remove(bike);
                _context.SaveChanges();
            }
        }

    }
}
