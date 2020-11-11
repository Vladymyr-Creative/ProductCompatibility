using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ProductCompatibility.Models;
using ProductCompatibility.Data.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using ProductCompatibility.Data;

namespace ProductCompatibility.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDBContent _dbContext;
        public AccountController(AppDBContent context)
        {
            _dbContext = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid) {
                User user = await _dbContext.User
                    .Include(u=>u.Role)
                    .FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);
                if (user != null) {
                    await Authenticate(user);

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid) {
                User user = await _dbContext.User.FirstOrDefaultAsync(u => u.Email == model.Email);
                if (user == null) {
                    // добавляем пользователя в бд
                    user = new User { Email = model.Email, Password = model.Password };
                    Role userRole = await _dbContext.Role.FirstOrDefaultAsync(r => r.Name == "user");
                    if (userRole != null)
                        user.Role = userRole;

                    _dbContext.User.Add(user);

                    await _dbContext.SaveChangesAsync();

                    await Authenticate(user);

                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        private async Task Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role?.Name)
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
