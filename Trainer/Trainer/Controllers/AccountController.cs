using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.Extensions.Localization;
using Trainer.Application.Aggregates.BaseUser.Commands.SignIn;
using Trainer.Application.Aggregates.BaseUser.Queries.GetBaseUser;
using Trainer.Application.Aggregates.OTPCodes.Commands.RequestLoginCode;
using Trainer.Application.Aggregates.OTPCodes.Commands.RequestRegistrationCode;
using Trainer.Application.Exceptions;
using Trainer.Common;
using Trainer.Common.TableConnect.Common;
using Trainer.Enums;
using Trainer.Models;

namespace Trainer.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IStringLocalizer<AccountController> Localizer;
        public AccountController(ILogger<AccountController> logger, IStringLocalizer<AccountController> localizer) : base(logger)
        {
            Localizer = localizer;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(SignInCommand command)
        {
            try
            {
                await Mediator.Send(command);
                await Mediator.Send(new RequestRegistrationCodeCommand
                {
                    Email = command.Email,
                    Host = HttpContext.Request.Host.ToString()
                });

                return RedirectToAction("VerifyCode", "OTP",
                    new { otpAction = OTPAction.Registration, email = command.Email });

            }
            catch (FluentValidation.ValidationException ex)
            {
                foreach (var modelValue in ModelState.Values)
                {
                    modelValue.Errors.Clear();
                }
                ModelState.AddModelError(string.Empty, Localizer[ex.Errors.First().ErrorMessage]);
            }

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
                    return RedirectToAction("VerifyCode", "OTP",
                        new { otpAction = OTPAction.Login, email = user.Email });
                }
                else
                {
                    ModelState.AddModelError("", Localizer["Error login/password"]);
                }
            }
            catch (NotFoundException ex)
            {
                ModelState.AddModelError("", Localizer["Error login/password"]);
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ReturnClaim(string Email)
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
