using Shop.BuisnesLogic.context;
using Shop.Domain.Entities;
using Shop.BuisnesLogic.service;
using System;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace CarShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserService _userService;
        private readonly Guid _adminRoleId = new Guid("6DAC8E22-F4DC-4FD8-8EE9-25B3CEFAA2FE");

        public ProductController()
        {
            _context = new AppDbContext();
            _userService = new UserService(); // Предполагается, что сервис предоставляет метод GetUserWithRoles
        }

        // GET: Product
        public ActionResult Index()
        {
            var bikes = _context.Bikes.ToList();
            return View(bikes);
        }

        // GET: Create
        public ActionResult Create()
        {
            // Проверка роли пользователя
            if (!IsAdmin())
            {
                return new HttpStatusCodeResult(403, "Доступ запрещен");
            }

            return View();
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Bike bike)
        {
            if (ModelState.IsValid)
            {
                _context.Bikes.Add(bike);
                _context.SaveChanges();

                TempData["SuccessMessage"] = "Товар успешно добавлен!";
                return RedirectToAction("Index");
            }

            return View(bike);
        }

        // Страница с подробным описанием велосипеда
        public ActionResult Details(Guid id)
        {
            var bike = _context.Bikes.FirstOrDefault(b => b.Id == id);
            if (bike == null)
            {
                return HttpNotFound();
            }

            // Проверяем роль пользователя
            ViewBag.CanDelete = IsAdmin();

            return View(bike);
        }



        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id)
        {
            // Проверка, является ли пользователь администратором
            if (!IsAdmin())
            {
                TempData["ErrorMessage"] = "У вас нет прав для удаления товара!";
                return RedirectToAction("Index");
            }

            var bike = _context.Bikes.FirstOrDefault(b => b.Id == id);
            if (bike == null)
            {
                TempData["ErrorMessage"] = "Товар не найден!";
                return RedirectToAction("Index");
            }

            try
            {
                _context.Bikes.Remove(bike);
                _context.SaveChanges(); // Сохраняем изменения в базе данных

                TempData["SuccessMessage"] = "Товар успешно удален!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Ошибка при удалении товара: {ex.Message}";
            }

            return RedirectToAction("Index"); // Перенаправление на страницу списка товаров
        }



        //// Страница подтверждения удаления товара
        //public ActionResult ConfirmDelete(Guid id)
        //{
        //    var bike = _context.Bikes.FirstOrDefault(b => b.Id == id);
        //    if (bike == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    return View(bike);
        //}

        //// Метод удаления товара
        //// Метод удаления товара
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(Guid id)
        //{
        //    var bike = _context.Bikes.FirstOrDefault(b => b.Id == id);
        //    if (bike == null)
        //    {
        //        TempData["ErrorMessage"] = "Товар не найден!";
        //        return RedirectToAction("Index");
        //    }

        //    try
        //    {
        //        _context.Bikes.Remove(bike);
        //        _context.SaveChanges(); // Сохраняем изменения в базе данных

        //        TempData["SuccessMessage"] = "Товар успешно удален!";
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["ErrorMessage"] = $"Ошибка при удалении товара: {ex.Message}";
        //    }

        //    return RedirectToAction("Index");
        //}



        // Проверка, является ли пользователь администратором
        private bool IsAdmin()
        {
            var currentUser = _userService.GetUserWithRoles(User.Identity.Name);
            return currentUser?.RoleId == _adminRoleId;
        }
    }
}
