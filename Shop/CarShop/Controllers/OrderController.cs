using Microsoft.AspNet.Identity;
using Shop.BuisnesLogic.service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CarShop.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly OrderService _orderService;
        private readonly UserService _userService;

        public OrderController()
        {
            _orderService = new OrderService();
            _userService = new UserService();
        }

        // Кастомный метод для получения ID авторизованного пользователя
        private Guid? GetAuthenticatedUserId(out string errorMessage)
        {
            errorMessage = null;

            // Проверяем, авторизован ли пользователь
            if (!User.Identity.IsAuthenticated)
            {
                errorMessage = "Пользователь не авторизован. Пожалуйста, войдите в систему.";
                return null;
            }

            // Пробуем получить ID пользователя из сессии
            if (Session["UserId"] != null)
            {
                if (Guid.TryParse(Session["UserId"].ToString(), out Guid userId))
                {
                    return userId;
                }
                else
                {
                    errorMessage = "ID пользователя в сессии имеет неверный формат.";
                    return null;
                }
            }

            // Если сессия пуста, ищем пользователя в базе по User.Identity.Name
            var user = _userService.GetAllUsers()
                .FirstOrDefault(u => u.Name == User.Identity.Name);

            if (user == null)
            {
                errorMessage = "Пользователь не найден в базе данных.";
                return null;
            }

            // Сохраняем ID в сессии для будущих запросов
            Session["UserId"] = user.Id;
            return user.Id;
        }

        // GET: Order
        public ActionResult Index()
        {
            return View();
        }

        // GET: /Order/Checkout
        // Страница оформления заказа
        public ActionResult Checkout()
        {
            return View();
        }

        // POST: /Order/Checkout
        // Обработка оформления заказа
        [HttpPost]
        public ActionResult Checkout(string returnUrl)
        {
            try
            {
                // Получаем ID пользователя
                string errorMessage;
                var userId = GetAuthenticatedUserId(out errorMessage);
                if (!userId.HasValue)
                {
                    return Json(new { success = false, error = errorMessage });
                }

                // Создаём заказ
                _orderService.CreateOrderFromCart(userId.Value);
                return Json(new { success = true, redirectUrl = Url.Action("Index", "Cart") });
            }
            catch (Exception ex)
            {
                // Логируем ошибку для отладки
                System.Diagnostics.Debug.WriteLine("Ошибка в Checkout: " + ex.Message);
                return Json(new { success = false, error = ex.Message });
            }
        }

        // GET: /Order/MyOrders
        // Страница "Мои заказы"
        public ActionResult MyOrders()
        {
            string errorMessage;
            var userId = GetAuthenticatedUserId(out errorMessage);
            if (!userId.HasValue)
            {
                return RedirectToAction("Login", "Account");
            }

            var orders = _orderService.GetUserOrders(userId.Value);
            return View(orders);
        }
    }
}