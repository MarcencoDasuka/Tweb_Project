using Shop.BuisnesLogic.context;
using Shop.BuisnesLogic.repository;
using Shop.BuisnesLogic.serviceInterface;
using Shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Shop.BuisnesLogic.service
{
    public class UserService : IUserService
    {
        private readonly UserRepository _userRepository;
        private readonly AppDbContext _context;

        public UserService()
        {
            _context = new AppDbContext();
            _userRepository = new UserRepository(_context); // Используем один контекст
        }

        // Основные методы через репозиторий
        public void AddUser(User user)
        {
            if (string.IsNullOrWhiteSpace(user.Name) || string.IsNullOrWhiteSpace(user.Password))
            {
                throw new ArgumentException("Имя пользователя и пароль не могут быть пустыми.");
            }
            _userRepository.Add(user);
        }

        public void UpdateUser(User user)
        {
            _userRepository.Update(user);
        }

        public void DeleteUser(Guid id)
        {
            _userRepository.Delete(id);
        }

        // Методы с Include для сложных запросов
        public IQueryable<User> GetAllUsersWithRoles()
        {
            return _context.Users
                .Include(u => u.Role)
                .AsQueryable();
        }

        public User GetUserWithRole(Guid id)
        {
            return _context.Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Id == id);
        }

        // Совместимость с интерфейсом (если нужно)
        public IEnumerable<User> GetAllUsers()
        {
            return _userRepository.GetAll();
        }

        public User GetUserById(Guid id)
        {
            return _userRepository.GetById(id);
        }

        public User GetUserWithRoles(string username)
        {
            return _context.Users
                .Include(u => u.Role) // Подгружаем связанную роль
                .FirstOrDefault(u => u.Name == username);
        }


        public User GetUserWithRoles(Guid id)
        {
            return _context.Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Id == id);
        }


    }
}