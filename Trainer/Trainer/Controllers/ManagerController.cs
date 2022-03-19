using Microsoft.AspNetCore.Mvc;
using Trainer.Application.Aggregates.Manager.Commands.SignInManager;
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
            return RedirectToAction("Index", "Home");
        }
    }
}
