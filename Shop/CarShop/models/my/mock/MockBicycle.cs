using CarShop.Models.my.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarShop.Models.my.mock
{
    public class MockBicycle : IAllBicycles
    {
        private readonly IBicycleCategory _categoryBicycle = new MockCategory();

        public IEnumerable<Bicycle> Bicycles
        {
            get
            {
                return new List<Bicycle> {
                    new Bicycle{id = 1, name = "A-52", shortDesc = "A reliable mountain bike", longDesc = "A-52 is perfect for off-road adventures with durable construction and superior handling.", img = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTpLB2l6UXlqixi1D3y6S2En3ECwVnVcNUFOQ&s", price = 444, isFavorite = true, available = true, category = _categoryBicycle.AllCategories.First()},
                    new Bicycle{id = 2, name = "Roadster", shortDesc = "A sleek road bike", longDesc = "Roadster offers unmatched speed and performance on paved surfaces.", img = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRn33TQ27UleJ7RpEIUaQF6WIBFDTg5bvGk7g&s", price = 599, isFavorite = true, available = true, category = _categoryBicycle.AllCategories.First()},
                    new Bicycle{id = 3, name = "EcoBike", shortDesc = "Eco-friendly city bike", longDesc = "EcoBike is designed for urban commuting with a lightweight frame and eco-friendly materials.", img = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRdpvHUfEwFBAOoI6PZVowGHmwtt0tzx0_Mmg&s", price = 350, isFavorite = false, available = true, category = _categoryBicycle.AllCategories.First()},
                    new Bicycle{id = 4, name = "Cruiser", shortDesc = "Comfortable cruiser bike", longDesc = "Cruiser provides a relaxed riding experience with its comfortable seat and smooth handling.", img = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTNlQ2WsmfBE6HQFrp9W2NRnoldlfJiuPJuZg&s", price = 275, isFavorite = false, available = true, category = _categoryBicycle.AllCategories.First()},
                    new Bicycle{id = 5, name = "TrailBlazer", shortDesc = "All-terrain trail bike", longDesc = "TrailBlazer is built for rugged trails with robust features for ultimate durability.", img = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR9-En5rQxvyCrIg3BXvkxA18usaOEx8kPyxQ&s", price = 499, isFavorite = true, available = true, category = _categoryBicycle.AllCategories.First()},
                    new Bicycle{id = 6, name = "Speedster", shortDesc = "High-speed racing bike", longDesc = "Speedster is engineered for competitive racing with advanced aerodynamics and high-end components.", img = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQqKyqIjWBW9J1CB3BKNRBo3cf_8fR9bpZD4g&s", price = 899, isFavorite = true, available = true, category = _categoryBicycle.AllCategories.First()}
                };
            }
        }
        public IEnumerable<Bicycle> getFavBicycles { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Bicycle getObjectBicycle(int bicycleId)
        {
            throw new NotImplementedException();
        }
    }
}
