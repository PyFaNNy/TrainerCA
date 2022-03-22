using Microsoft.AspNetCore.Mvc;
using Trainer.Application.Aggregates.Doctor.Commands.SignInDoctor;
using Trainer.Application.Aggregates.OTPCodes.Commands.RequestRegistrationCode;
using Trainer.Models;

namespace Trainer.Controllers
{
    public class DoctorController : BaseController
    {
        public DoctorController(ILogger<DoctorController> logger)
        : base(logger)
        {
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(RegisterViewModel model)
        {
            await Mediator.Send(new SignInDoctorCommand
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
