using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Trainer.Domain.Entities.User;
using Trainer.Enums;
using Trainer.Models;

namespace Trainer.Controllers
{
    public class AccountController : BaseController
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(ILogger<AccountController> logger, UserManager<User> userManager, SignInManager<User> signInManager) : base(logger)
        {
            _userManager = userManager ?? throw new ArgumentNullException($"{nameof(userManager)} is null.");
            _signInManager = signInManager ?? throw new ArgumentNullException($"{nameof(signInManager)} is null.");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    MiddleName = model.MiddleName,
                    Email = model.Email,
                    UserName = model.UserName,
                    Status = StatusUser.Active
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                await _userManager.AddToRoleAsync(user, "unknown");
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
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
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);
                    if (result.Succeeded)
                    {
                       var res = await _userManager.UpdateAsync(user);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Incorrect login and (or) password");
                    }
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
