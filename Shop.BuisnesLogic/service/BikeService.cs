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
    public class BikeService : IBikeService
    {
        private readonly BikeRepository _bikeRepository;

        // Конструктор вручную создаёт экземпляр BikeRepository
        public BikeService()
        {
            _bikeRepository = new BikeRepository(new AppDbContext());
        }

        public IEnumerable<Bike> GetAllBikes()
        {
            return _bikeRepository.GetAll();
        }

        public Bike GetBikeById(Guid id)
        {
            return _bikeRepository.GetById(id);
        }

        public void AddBike(Bike bike)
        {
            // Пример валидации
            if (bike.Price <= 0)
            {
                throw new ArgumentException("Цена велосипеда должна быть больше нуля.");
            }
            _bikeRepository.Add(bike);
        }

        public void UpdateBike(Bike bike)
        {
            _bikeRepository.Update(bike);
        }

        public void DeleteBike(Guid id)
        {
            _bikeRepository.Delete(id);
        }

    }
}
