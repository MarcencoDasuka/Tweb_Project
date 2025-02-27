using CarShop.Models.my.mock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly MockBicycle _allBicycles = new MockBicycle();
        private readonly MockCategory _allCategories = new MockCategory();

        public HomeController()
        {
            // Убедитесь, что объекты инициализированы
        }

        // GET: Home
        public ActionResult Index()
        {
            var bicycles = _allBicycles.Bicycles;
            return View(bicycles);
        }
    }
}
