using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Trainer.Application.Aggregates.BaseUser.Queries.GetBaseUser;
using Trainer.Application.Aggregates.OTPCodes.Commands.RequestLoginCode;
using Trainer.Common;
using Trainer.Common.TableConnect.Common;
using Trainer.Enums;
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

        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await Mediator.Send(new GetBaseUserQuery(model.UserName));
                    var result = CryptoHelper.VerifyHashedPassword(user.PasswordHash, model.Password);

                    if (result)
                    {
                        await Mediator.Send(new RequestLoginCodeCommand
                        {
                            Email = user.Email,
                            Host = HttpContext.Request.Host.ToString()
                        });
                        return RedirectToAction("VerifyCode", "OTP", new { otpAction = OTPAction.Login, email = user.Email });
                    }
                    else
                    {
                        ModelState.AddModelError("All", "Error login/password");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("All", ex.Message);
                }
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ReturnClaim(string Email, string Password)
        {
            var user = await Mediator.Send(new GetBaseUserQuery(Email));
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToName())
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(new ClaimsPrincipal(claimsIdentity));

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
