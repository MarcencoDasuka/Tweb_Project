using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Entities
{
    [Table("CartItems")]
    public class CartItem
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid CartId { get; set; }  // Внешний ключ на корзину

        [Required]
        public Guid BikeId { get; set; }  // Внешний ключ на товар (велосипед)

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Количество должно быть не менее 1")]
        public int Quantity { get; set; } = 1;

        // Навигационные свойства
        [ForeignKey("CartId")]
        public virtual Cart Cart { get; set; }

        [ForeignKey("BikeId")]
        public virtual Bike Bike { get; set; }
    }
}
