using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarShop.Models
{
    public class Model{
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

       public List<Biciclete> IdBike {  get; set; }
        

    }
}