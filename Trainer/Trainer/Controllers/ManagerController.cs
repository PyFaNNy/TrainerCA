using Microsoft.AspNetCore.Mvc;
using Trainer.Application.Aggregates.Manager.Commands.SignInManager;

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
        public async Task<IActionResult> SignIn(SignInManagerCommand command)
        {
            await Mediator.Send(command);
            return RedirectToAction("Index", "Home");
        }
    }
}
