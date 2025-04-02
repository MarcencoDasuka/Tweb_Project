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
            }
        }
    }
