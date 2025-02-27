using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShop.Models.my.interfaces
{
    interface IAllBicycles
    {

        IEnumerable<Bicycle> Bicycles { get; }

        IEnumerable<Bicycle> getFavBicycles { get; set; }

        Bicycle getObjectBicycle(int bicycleId);

    }
}
