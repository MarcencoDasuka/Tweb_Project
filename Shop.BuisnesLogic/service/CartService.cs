using Shop.BuisnesLogic.context;
using Shop.BuisnesLogic.intefaces;
using Shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.BuisnesLogic.service
{
    public class CartService
    {
        private readonly AppDbContext _context;
        private readonly ICartRepository _cartRepository;
        private readonly IBikeRepository _bikeRepository; // Для проверки существования товара

        public CartService(ICartRepository cartRepository, IBikeRepository bikeRepository, AppDbContext context)
        {
            _cartRepository = cartRepository;
            _bikeRepository = bikeRepository;
            _context = context;
        }

        public Cart GetUserCart(Guid userId)
        {
            return _cartRepository.GetByUserId(userId) ?? new Cart { UserId = userId };
        }

        public void AddToCart(Guid userId, Guid bikeId, int quantity = 1)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be positive");

            // Проверяем существование товара
            if (_bikeRepository.GetById(bikeId) == null)
                throw new InvalidOperationException("Bike not found");

            _cartRepository.AddItem(userId, bikeId, quantity);
        }

        public void UpdateItemQuantity(Guid itemId, int newQuantity)
        {
            if (newQuantity <= 0)
                throw new ArgumentException("Quantity must be positive");

            _cartRepository.UpdateItemQuantity(itemId, newQuantity);
        }

        public void RemoveFromCart(Guid itemId)
        {
            _cartRepository.RemoveItem(itemId);
        }

        public void ClearCart(Guid userId)
        {
            _cartRepository.ClearCart(userId);
        }

        public decimal CalculateTotal(Guid userId)
        {
            var cart = GetUserCart(userId);
            return cart.Items?.Sum(item => item.Bike.Price * item.Quantity) ?? 0;
        }

        public Cart GetCart(Guid userId)
        {
            return _context.Carts
                .Include("Items.Bike")
                .FirstOrDefault(c => c.UserId == userId);
        }

        public Cart GetByUserId(Guid userId)
        {
            return _context.Carts
                .Include("Items.Bike")
                .FirstOrDefault(c => c.UserId == userId);
        }
    }
}
