using Microsoft.AspNetCore.Mvc;
using Trainer.Models;

namespace Trainer.Controllers
{
    public class AccountController : BaseController
    {
        public AccountController(ILogger<AccountController> logger) : base(logger)
        {
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}
