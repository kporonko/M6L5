using M6L5.Core.Models;
using M6L5.Core.Services;
using M6L5.Core.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace M6L5.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;
        public AccountController(ILogger<HomeController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Login(UserLogin loginUser)
        {
            if (ModelState.IsValid)
            {
                User user = _userService.GetAll().FirstOrDefault(u => u.Login == loginUser.Login && u.Password == loginUser.Password);
                if (user != null)
                {
                    await Authenticate(loginUser.Login);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, "Incorrect login or password");
            }
            return View(loginUser);
        }

        private async Task Authenticate(string login)
        {
            var claims = new List<Claim>{ new Claim(ClaimsIdentity.DefaultNameClaimType, login) };
            ClaimsIdentity identity = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Register(UserRegister registerUser)
        {
            if (ModelState.IsValid)
            {
                User user = _userService.GetAll().FirstOrDefault(u => u.Login == registerUser.Login && u.Password == registerUser.Password);
                if (user == null)
                {
                    _userService.Create(new User { Id = _userService.GetLastId() + 1, Login = registerUser.Login, Password = registerUser.Password });
                    await Authenticate(registerUser.Login);
                    return RedirectToAction("Login", "Account");
                }
                ModelState.AddModelError(string.Empty, "Incorrect login or password");
            }
            return View(registerUser);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
