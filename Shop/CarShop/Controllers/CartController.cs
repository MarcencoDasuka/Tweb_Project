using Shop.BuisnesLogic.context;
using Shop.BuisnesLogic.repository;
using Shop.BuisnesLogic.service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CarShop.Controllers
{
    public class CartController : Controller
    {
        private readonly CartService _cartService;

        public CartController()
        {
            // Создаем зависимости вручную
            var dbContext = new AppDbContext(); // Контекст БД
            var cartRepo = new CartRepository(dbContext); // Репозиторий корзины
            var bikeRepo = new BikeRepository(dbContext); // Репозиторий товаров
            _cartService = new CartService(cartRepo, bikeRepo, dbContext); // Передаём AppDbContext
        }

        public ActionResult Index()
        {
            var userId = GetCurrentUserId();
            var cart = _cartService.GetUserCart(userId);
            return View(cart.Items);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddToCart(Guid bikeId, int quantity = 1)
        {
            try
            {
                if (quantity <= 0)
                {
                    TempData["ErrorMessage"] = "Количество должно быть больше нуля.";
                    return RedirectToAction("Details", "Product", new { id = bikeId });
                }

                var userId = GetCurrentUserId();
                _cartService.AddToCart(userId, bikeId, quantity);
                TempData["SuccessMessage"] = "Товар добавлен в корзину!";
                return RedirectToAction("Index");
            }
            catch (InvalidOperationException ex)
            {
                TempData["ErrorMessage"] = ex.Message; // Например, "Bike not found"
                return RedirectToAction("Details", "Product", new { id = bikeId });
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Произошла ошибка при добавлении товара в корзину.";
                return RedirectToAction("Details", "Product", new { id = bikeId });
            }
        }

        private Guid GetCurrentUserId()
        {
            if (Session["UserId"] == null)
            {
                Session["UserId"] = Guid.NewGuid(); // Для гостевых пользователей
            }
            return (Guid)Session["UserId"];
        }

        [HttpPost]
        public ActionResult UpdateQuantity(Guid itemId, int quantity)
        {
            _cartService.UpdateItemQuantity(itemId, quantity);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpPost]
        public ActionResult RemoveItem(Guid itemId)
        {
            _cartService.RemoveFromCart(itemId);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}