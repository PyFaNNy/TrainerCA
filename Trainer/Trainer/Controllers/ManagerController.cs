using Microsoft.AspNetCore.Mvc;
using Trainer.Application.Aggregates.Manager.Commands.SignInManager;
using Trainer.Application.Aggregates.OTPCodes.Commands.RequestRegistrationCode;
using Trainer.Models;

namespace Trainer.Controllers
{
    public class ManagerController : BaseController
    {
        public ManagerController(ILogger<DoctorController> logger)
        : base(logger)
        {
        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(RegisterViewModel model)
        {
            await Mediator.Send(new SignInManagerCommand
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                MiddleName = model.MiddleName,
                Email = model.Email,
                Password = model.Password,
                ConfirmPassword = model.ConfirmPassword,
            });
            await Mediator.Send(new RequestRegistrationCodeCommand
            {
                Email = model.Email,
                Host = HttpContext.Request.Host.ToString()
            });
            return RedirectToAction("Index", "Home");
        }
    }
}
