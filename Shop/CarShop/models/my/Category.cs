using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarShop.Models.my
{
	public class Category
	{

		public int id { get; set; }

		public string categoryName { get; set; }

		public string desc { get; set; }

		 public List<Bicycle> bicycles {get; set; }

	}
}