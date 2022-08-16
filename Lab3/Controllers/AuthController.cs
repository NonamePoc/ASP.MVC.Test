using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Lab3.Data;
using Lab3.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace Lab3.Controllers
{
    public class AuthController : Controller
    {
        private readonly DataContext _dataContext;

        public AuthController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View();

            foreach (var user in _dataContext.Users)
            {
                if (user.Login == viewModel.Login && user.Password == viewModel.Password)
                {
                    await AuthenticateAsync(viewModel);
                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError("Login", "Неправельний логін або пароль");
            ModelState.AddModelError("Password", "Неправельний логін або пароль");


            return View();
        }

        private async Task AuthenticateAsync(LoginViewModel viewModel)
        {
            var claims = new List<Claim>
            {
                new Claim("login", viewModel.Login),
            };

            var id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}