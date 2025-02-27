using CarShop.Models.my.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarShop.Models.my.mock
{
    public class MockCategory : IBicycleCategory
    {
        public IEnumerable<Category> AllCategories
        {
            get
            {
                return new List<Category> {
                    new Category {categoryName = "Горный", desc = "Для езды по не ровным поверхностям"},
                    new Category {categoryName = "Скоростной", desc = "Для гонок"}
                };
            }
        }
    }
}