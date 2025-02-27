using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarShop.Models.my
{
	public class Bicycle
	{
        public int id { get; set; }

        public string name { get; set; }

        public string longDesc { get; set; }

        public string shortDesc { get; set; }
   
        public ushort price { get; set; }

        public Boolean available { get; set; }

        public Boolean isFavorite { get; set; }

        public string img { get; set; }

       // public int categoryId { get; set; }

        public virtual Category category { get; set; }
    }
}