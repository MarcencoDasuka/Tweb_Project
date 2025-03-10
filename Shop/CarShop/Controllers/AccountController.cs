using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;

namespace CarShop.Controllers
{
    public class AccountController : Controller
    {
        // GET: /Account/Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        public ActionResult Register(FormCollection form)
        {
            string username = form["Username"];
            string email = form["Email"];
            string password = form["Password"];

            // Здесь будет логика сохранения пользователя

            return RedirectToAction("Login"); // После регистрации перенаправляем на вход
        }

        // GET: /Account/Login
        public ActionResult Login()
        {
            return View();
        }


        // GET: /Account/Profile
        public ActionResult Profile()
        {
            return View();
        }
    }
}