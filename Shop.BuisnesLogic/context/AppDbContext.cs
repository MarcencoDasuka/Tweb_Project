using Shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.BuisnesLogic.context
{
    public class AppDbContext : DbContext
    {
        // Конструктор для указания строки подключения
        public AppDbContext() : base("Shop")
        {
        }

        // DbSet для твоих сущностей
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Bike> Bikes { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        // Переопределение метода для настройки моделей
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Настройка для User и Role (Optional связь)
            modelBuilder.Entity<User>()
                .HasOptional(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId)
                .WillCascadeOnDelete(false);

            // Настройка связей Bike и User (Optional связь)
            modelBuilder.Entity<Bike>()
                .HasOptional(b => b.User)
                .WithMany(u => u.Bikes)
                .HasForeignKey(b => b.UserId)
                .WillCascadeOnDelete(false);

            // Корзина принадлежит одному пользователю
            modelBuilder.Entity<Cart>()
                .HasRequired(c => c.User)
                .WithMany()  // У User нет коллекции Cart (однонаправленная связь)
                .HasForeignKey(c => c.UserId)
                .WillCascadeOnDelete(true);  // Удаление пользователя → удаление его корзины

            // Элемент корзины связан с корзиной и товаром
            modelBuilder.Entity<CartItem>()
                .HasRequired(ci => ci.Cart)
                .WithMany(c => c.Items)
                .HasForeignKey(ci => ci.CartId)
                .WillCascadeOnDelete(true);  // Удаление корзины → удаление её элементов

            modelBuilder.Entity<CartItem>()
                .HasRequired(ci => ci.Bike)
                .WithMany()  // У Bike нет ссылки на CartItem
                .HasForeignKey(ci => ci.BikeId)
                .WillCascadeOnDelete(false);  // Товар нельзя удалить, если он в корзине
        }
    }
}
