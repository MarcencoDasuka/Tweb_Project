using Shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.BuisnesLogic.intefaces
{
    public interface ICartRepository
    {
        Cart GetByUserId(Guid userId);
        CartItem GetItem(Guid cartId, Guid itemId);
        void AddItem(Guid userId, Guid bikeId, int quantity = 1);
        void UpdateItemQuantity(Guid itemId, int newQuantity);
        void RemoveItem(Guid itemId);
        void ClearCart(Guid userId);
    }
}
