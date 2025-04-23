using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Entities
{
    [Table("Bikes")]
     public class Bike
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Name is required")]
        [StringLength(64)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(512)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Short Description is required")]
        [StringLength(512)]
        public string ShortDescription { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "ImageUrl is required")]
        public string ImageUrl { get; set; }

        // Связь с пользователем
        public Guid? UserId { get; set; } // Внешний ключ

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

    }
}
