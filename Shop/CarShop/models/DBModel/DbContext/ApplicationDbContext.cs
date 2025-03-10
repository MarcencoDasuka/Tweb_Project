using CarShop.Models.DBModel.Entities;
using CarShop.Models.my;
using System.Data.Entity;

namespace CarShop.Models.DBModel.DataContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("name=CarShop")
        {
        }

        public DbSet<Bicycle> Bicycles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
