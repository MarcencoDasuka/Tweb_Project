using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShop.Models.my.interfaces
{
    interface IBicycleCategory
    {

        IEnumerable<Category> AllCategories { get; }

    }
}
