using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;

using Shop.BuisnesLogic.core;
using Shop.BuisnesLogic.interfaces;
using Shop.BuisnesLogic;
using Shop.Domain.Entities.UserDTO;
using Shop.Domain.Entities.User;
//using Shop.Domain.Entities.User;

namespace CarShop.Controllers
{
    public class AccountController : Controller
    {

        private readonly ISession _session;
        public AccountController()
        {
            var bl = new BuisnesLogic();
            _session = bl.GetSessionBL();
        }


        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UserLoginDTO login)
        {
            if (ModelState.IsValid)
            {
                ULoginData data = new ULoginData 
                {
                    Credential = login.Credential,
                    Password = login.Password,
                    LoginIp = Request.UserHostAddress,
                    LoginDateTime = DateTime.Now
                };

                var userLogin = _session.UserLoginDTO(data);
                if (userLogin.Status)
                {
                    // ADD COOKIE

                    return RedirectToAction("Index", "Home");
                }
                else 
                {
                    ModelState.AddModelError("", userLogin.StatusMsg);
                    return View();
                }
            }

            return View();
        }

        // POST: /Account/Register
        //[HttpPost]
        //public ActionResult Register(FormCollection form)
        //{
        //    // Здесь будет логика сохранения пользователя

        //    return RedirectToAction("Login"); // После регистрации перенаправляем на вход
        //}

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