namespace Shop.BuisnesLogic.Migrations
{
    using Shop.Domain.Entities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Shop.BuisnesLogic.context.AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Shop.BuisnesLogic.context.AppDbContext context)
        {
            // Создаем роли, если их нет
            if (!context.Roles.Any(r => r.Name == "User"))
            {
                context.Roles.AddOrUpdate(new Role
                {
                    Name = "User",
                    Description = "Роль для обычных пользователей."
                });
            }

            Role adminRole;
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                adminRole = new Role
                {
                    Name = "Admin",
                    Description = "Роль для администраторов системы."
                };
                context.Roles.AddOrUpdate(adminRole);
            }
            else
            {
                adminRole = context.Roles.First(r => r.Name == "Admin");
            }

            // Проверяем существование администратора
            if (!context.Users.Any(u => u.Name == "Admin"))
            {
                context.Users.AddOrUpdate(new User
                {
                    Name = "Admin",
                    Password = "123456789", 
                    RoleId = adminRole.Id
                });
            }

            context.SaveChanges();
        }
    }
}
