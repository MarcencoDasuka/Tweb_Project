using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarShop.Controllers
{
    public class ProductController : Controller
    {



        // GET: Product
        public ActionResult Index()
        {
            return View();
        }


        // GET: Details
        // Метод для отображения деталей велосипеда
        public ActionResult Details()
        {
            return View(); 
        }


        public ActionResult Cart()
        {
            return View();
        }
    }
}