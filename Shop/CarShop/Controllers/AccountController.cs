using Shop.BuisnesLogic.service;
using Shop.Domain.Entities;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CarShop.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserService _userService;
        private readonly RoleService _roleService;

        public AccountController()
        {
            _userService = new UserService();
            _roleService = new RoleService();
        }

        // Регистрация пользователя
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(string login, string password, string confirmPassword)
        {
            try
            {
                // Проверка совпадения паролей
                if (password != confirmPassword)
                {
                    ModelState.AddModelError("", "Пароли не совпадают");
                    return View();
                }

                // Проверка существования пользователя
                if (_userService.GetAllUsers().Any(u => u.Name == login))
                {
                    ModelState.AddModelError("", "Пользователь с таким логином уже существует");
                    return View();
                }

                // Получаем роль User (или создаем если не существует)
                var userRole = _roleService.GetAllRoles().FirstOrDefault(r => r.Name == "User");
                if (userRole == null)
                {
                    userRole = new Role { Name = "User", Description = "Обычный пользователь" };
                    _roleService.AddRole(userRole);
                }

                // Создаем нового пользователя
                var newUser = new User
                {
                    Name = login,
                    Password = password, // В реальном проекте пароль нужно хэшировать!
                    RoleId = userRole.Id
                };

                _userService.AddUser(newUser);

                // Перенаправляем на страницу входа
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ошибка при регистрации: " + ex.Message);
                return View();
            }
        }



        //=======================================
        // GET: Вход в систему
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string login, string password)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
                {
                    ModelState.AddModelError("", "Логин и пароль обязательны для заполнения");
                    return View();
                }

                var user = _userService.GetAllUsers()
                    .FirstOrDefault(u => u.Name == login && u.Password == password);

                if (user == null)
                {
                    ModelState.AddModelError("", "Неверный логин или пароль");
                    return View();
                }

                // 1. Создаем билет аутентификации
                var authTicket = new FormsAuthenticationTicket(
                    version: 1,
                    name: user.Name,
                    issueDate: DateTime.Now,
                    expiration: DateTime.Now.AddMinutes(30), // Время жизни
                    isPersistent: false,
                    userData: "" // Дополнительные данные
                );

                // 2. Шифруем билет
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

                // 3. Создаем куки
                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket)
                {
                    HttpOnly = true,
                    Secure = FormsAuthentication.RequireSSL,
                    Domain = FormsAuthentication.CookieDomain
                };

                // 4. Добавляем куки в ответ
                Response.Cookies.Add(authCookie);

                // 5. Перенаправляем НА ПРОФИЛЬ (а не на Index)
                return RedirectToAction("Profile", "Account");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ошибка при входе: " + ex.Message);
                return View();
            }
        }

        // Выход из системы
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
        //=============================================================

        [Authorize]
        public ActionResult Profile()
        {
            // Проверка наличия аутентификации
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login");
            }

            var currentUser = _userService.GetAllUsers()
                .FirstOrDefault(u => u.Name == User.Identity.Name);

            if (currentUser == null)
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("Login");
            }

            return View(currentUser);
        }
    }
}