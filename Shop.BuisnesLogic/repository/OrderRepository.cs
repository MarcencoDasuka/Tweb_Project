using Shop.BuisnesLogic.context;
using Shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Shop.BuisnesLogic.repository
{
    public class OrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public void CreateOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public Order GetOrderById(Guid orderId)
        {
            return _context.Orders
                .Include("Items.Bike")
                .FirstOrDefault(o => o.Id == orderId);
        }

        public void ClearCart(Guid userId)
        {
            var cart = _context.Carts
                .Include(c => c.Items)
                .FirstOrDefault(c => c.UserId == userId);

            if (cart != null)
            {
                _context.CartItems.RemoveRange(cart.Items);
                _context.Carts.Remove(cart);
                _context.SaveChanges();
            }
        }
    }
}
