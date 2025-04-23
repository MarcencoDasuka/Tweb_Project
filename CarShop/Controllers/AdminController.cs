using Shop.BuisnesLogic.context;
using Shop.BuisnesLogic.service;
using Shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarShop.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserService _userService;
        private readonly RoleService _roleService;
        private readonly Guid _adminRoleId = new Guid("6dac8e22-f4dc-4fd8-8ee9-25b3cefaa2fe");

        public AdminController()
        {
            _userService = new UserService();
            _roleService = new RoleService();
            _context = new AppDbContext();
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            var currentUser = _userService.GetUserWithRoles(User.Identity.Name);
            if (currentUser?.RoleId != _adminRoleId)
            {
                filterContext.Result = new HttpStatusCodeResult(403);
                return;
            }
        }

        // Главная страница (список пользователей)
        public ActionResult Index()
        {
            var users = _userService.GetAllUsersWithRoles()
                          .Where(u => u.RoleId != _adminRoleId) // Скрываем других админов
                          .OrderBy(u => u.Name)
                          .ToList();
            return View(users);
        }

        // Форма создания пользователя
        public ActionResult Create()
        {
            // Получаем список ролей, исключая роль Admin
            var roles = _roleService.GetAllRoles()
                          .Where(r => r.Id != _adminRoleId)
                          .Select(r => new SelectListItem
                          {
                              Value = r.Id.ToString(),
                              Text = r.Name
                          }).ToList();

            ViewBag.RoleId = new SelectList(roles, "Value", "Text");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user, Guid roleId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    user.Id = Guid.NewGuid();
                    user.RoleId = roleId;
                    _userService.AddUser(user);

                    TempData["SuccessMessage"] = "Пользователь успешно создан";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Ошибка: {ex.Message}");
            }

            // Повторно заполняем список ролей при ошибке
            var roles = _roleService.GetAllRoles()
                          .Where(r => r.Id != _adminRoleId)
                          .Select(r => new SelectListItem
                          {
                              Value = r.Id.ToString(),
                              Text = r.Name
                          }).ToList();

            ViewBag.RoleId = new SelectList(roles, "Value", "Text", roleId);
            return View(user);
        }


        // Подтверждение удаления
        public ActionResult Delete(Guid id)
        {
            var user = _userService.GetUserWithRoles(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            // Защита от удаления самого себя
            if (user.Name == User.Identity.Name)
            {
                TempData["ErrorMessage"] = "Вы не можете удалить себя!";
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // Обработка удаления
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            try
            {
                _userService.DeleteUser(id);
                TempData["SuccessMessage"] = "Пользователь удален";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Ошибка: {ex.Message}";
            }

            return RedirectToAction("Index");
        }
    }
}