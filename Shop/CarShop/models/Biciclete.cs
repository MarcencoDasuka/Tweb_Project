using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarShop.Models
{
    public class Biciclete{
        public int Id { get; set; }

        public string Description { get; set; } 

        public string ShortDescription { get; set; }

        public ushort Price { get; set; }

        public Boolean Avalible { get; set; }

        public String CategoryId { get; set; }



    }
}