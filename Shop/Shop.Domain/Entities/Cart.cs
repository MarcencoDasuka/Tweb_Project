using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Entities
{

    [Table("Carts")]
    public class Cart
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid UserId { get; set; }  // Внешний ключ на пользователя

        [ForeignKey("UserId")]
        public virtual User User { get; set; }  // Навигационное свойство

        // Список товаров в корзине
        public virtual ICollection<CartItem> Items { get; set; } = new List<CartItem>();

        // Вычисляемое свойство (не хранится в БД)
        [NotMapped]
        public decimal TotalPrice => Items?.Sum(item => item.Bike.Price * item.Quantity) ?? 0;
    }
}
