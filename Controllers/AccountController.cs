using Microsoft.AspNetCore.Mvc;
using ControlGastosWebApp.Models;
using ControlGastosWebApp.Data;

namespace ControlGastosWebApp.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (username == "admin" && password == "admin")
            {
                HttpContext.Session.SetString("User", username);
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Error = "Usuario o contraseña incorrectos.";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("User");
            return RedirectToAction("Login");
        }
    }
}
