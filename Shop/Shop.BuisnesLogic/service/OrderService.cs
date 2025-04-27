using Shop.BuisnesLogic.context;
using Shop.BuisnesLogic.repository;
using Shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity; // Главное пространство имен для Entity Framework


namespace Shop.BuisnesLogic.service
{
    public class OrderService
    {
        private readonly OrderRepository _orderRepository;

        public OrderService()
        {
            _orderRepository = new OrderRepository();
        }

        public void CreateOrderFromCart(Guid userId)
        {
            using (var context = new AppDbContext())
            {
                // Находим корзину пользователя с товарами
                var cart = context.Carts
                    .Include(c => c.Items.Select(ci => ci.Bike))
                    .FirstOrDefault(c => c.UserId == userId);

                if (cart == null || !cart.Items.Any())
                    throw new Exception("Корзина пуста");

                // Создаём новый заказ
                var order = new Order
                {
                    UserId = userId,
                    TotalPrice = cart.TotalPrice,
                    Items = cart.Items.Select(ci => new OrderItem
                    {
                        BikeId = ci.BikeId,
                        Quantity = ci.Quantity,
                        Price = ci.Bike.Price
                    }).ToList()
                };

                // Сохраняем заказ
                _orderRepository.Create(order);

                // Очищаем корзину
                context.CartItems.RemoveRange(cart.Items);
                context.Carts.Remove(cart);
                context.SaveChanges();
            }
        }

        public IEnumerable<Order> GetUserOrders(Guid userId)
        {
            return _orderRepository.GetOrdersByUserId(userId);
        }
    }
}
