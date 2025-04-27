using Shop.BuisnesLogic.context;
using Shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity; // Главное пространство имен для Entity Framework


namespace Shop.BuisnesLogic.repository
{
    public class OrderRepository
    {
        public void Create(Order order)
        {
            using (var context = new AppDbContext())
            {
                context.Orders.Add(order);
                context.SaveChanges();
            }
        }

        public IEnumerable<Order> GetOrdersByUserId(Guid userId)
        {
            using (var context = new AppDbContext())
            {
                return context.Orders
                    .Include(o => o.Items.Select(oi => oi.Bike))
                    .Where(o => o.UserId == userId)
                    .ToList();
            }
        }
    }
}
