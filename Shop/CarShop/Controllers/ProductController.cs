using CarShop.Models.my;
using CarShop.Models.my.interfaces;
using CarShop.Models.my.mock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly MockBicycle _allBicycles = new MockBicycle();
        private readonly MockCategory _allCategories = new MockCategory();

        public ProductController()
        {
            // Убедитесь, что объекты инициализированы
        }


        // GET: Product
        public ActionResult Index()
        {
            var bicycles = _allBicycles.Bicycles;
            return View(bicycles);
        }


        // GET: Details
        // Метод для отображения деталей велосипеда
        public ActionResult Details(int id)
        {
            // Найти велосипед с указанным id
            var bicycle = _allBicycles.Bicycles.FirstOrDefault(b => b.id == id);

            if (bicycle == null)
            {
                return HttpNotFound(); // Вернуть ошибку 404, если велосипед не найден
            }

            return View(bicycle); // Передать модель велосипеда в представление
        }


        public ActionResult Cart()
        {
            var cartItems = _allBicycles.Bicycles.Where(b => b.isFavorite); // Пример данных
            return View(cartItems);
        }
    }
}