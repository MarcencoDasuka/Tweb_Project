using Shop.BuisnesLogic.context;
using Shop.BuisnesLogic.intefaces;
using Shop.Domain.Entities;
using System;
using System.Data.Entity; // Главное пространство имен для Entity Framework
using System.Linq;

namespace Shop.BuisnesLogic.repository
{
    public class CartRepository : ICartRepository
    {
        private readonly AppDbContext _context;

        public CartRepository(AppDbContext context)
        {
            _context = context;
        }

        public Cart GetByUserId(Guid userId)
        {
            return _context.Carts
                .Include(c => c.Items.Select(i => i.Bike))
                .FirstOrDefault(c => c.UserId == userId);
        }

        public CartItem GetItem(Guid cartId, Guid itemId)
        {
            return _context.CartItems
                .Include(i => i.Bike)
                .FirstOrDefault(i => i.CartId == cartId && i.Id == itemId);
        }

        public void AddItem(Guid userId, Guid bikeId, int quantity = 1)
        {
            var cart = _context.Carts.FirstOrDefault(c => c.UserId == userId);
            if (cart == null)
            {
                cart = new Cart { UserId = userId };
                _context.Carts.Add(cart);
            }

            var existingItem = _context.CartItems
                .FirstOrDefault(i => i.CartId == cart.Id && i.BikeId == bikeId);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                _context.CartItems.Add(new CartItem
                {
                    CartId = cart.Id,
                    BikeId = bikeId,
                    Quantity = quantity
                });
            }
            _context.SaveChanges();
        }

        public void UpdateItemQuantity(Guid itemId, int newQuantity)
        {
            var item = _context.CartItems.Find(itemId);
            if (item != null)
            {
                item.Quantity = newQuantity;
                _context.SaveChanges();
            }
        }

        public void RemoveItem(Guid itemId)
        {
            var item = _context.CartItems.Find(itemId);
            if (item != null)
            {
                _context.CartItems.Remove(item);
                _context.SaveChanges();
            }
        }

        public void ClearCart(Guid userId)
        {
            var cart = _context.Carts.FirstOrDefault(c => c.UserId == userId);
            if (cart != null)
            {
                _context.CartItems.RemoveRange(cart.Items);
                _context.SaveChanges();
            }
        }
    }
}
