using Shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.BuisnesLogic.serviceInterface
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUsers();
        User GetUserById(Guid id);
        IQueryable<User> GetAllUsersWithRoles();
        User GetUserWithRole(Guid id);
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(Guid id);

        User GetUserWithRoles(string username);
        User GetUserWithRoles(Guid id);


    }
}
