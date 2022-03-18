using Microsoft.AspNetCore.Mvc;
using Trainer.Application.Aggregates.Doctor.Commands.SignInDoctor;

namespace Trainer.Controllers
{
    public class DoctorController : BaseController
    {
        public DoctorController(ILogger<DoctorController> logger)
        : base(logger)
        {
        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInDoctorCommand command)
        {
            await Mediator.Send(command);
            return RedirectToAction("Index", "Home");
        }
    }
}
