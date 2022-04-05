using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Trainer.Application.Aggregates.BaseUser.Commands.ApproveUser;
using Trainer.Application.Aggregates.BaseUser.Commands.BlockUser;
using Trainer.Application.Aggregates.BaseUser.Commands.ConfirmEmail;
using Trainer.Application.Aggregates.BaseUser.Commands.DeclineUser;
using Trainer.Application.Aggregates.BaseUser.Commands.DeleteUser;
using Trainer.Application.Aggregates.BaseUser.Commands.ResetPasswordUser;
using Trainer.Application.Aggregates.BaseUser.Commands.SignIn;
using Trainer.Application.Aggregates.BaseUser.Commands.UnBlockUser;
using Trainer.Application.Aggregates.BaseUser.Queries.GetBaseUsers;
using Trainer.Application.Aggregates.OTPCodes.Commands.RequestRegistrationCode;
using Trainer.Enums;

namespace Trainer.Controllers
{
    public class BaseUserController : BaseController
    {
        private readonly IStringLocalizer<BaseUserController> Localizer;

        public BaseUserController(ILogger<BaseUserController> logger, IStringLocalizer<BaseUserController> lcalizer)
            : base(logger)
        {
            Localizer = lcalizer;
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> GetModels(
            SortState sortOrder = SortState.FirstNameSort,
            int? pageIndex = 1,
            int? pageSize = 10)
        {
            ViewData["EmailSort"] = sortOrder == SortState.EmailSort ? SortState.EmailSortDesc : SortState.EmailSort;
            ViewData["FirstNameSort"] = sortOrder == SortState.FirstNameSort
                ? SortState.FirstNameSortDesc
                : SortState.FirstNameSort;
            ViewData["LastNameSort"] = sortOrder == SortState.LastNameSort
                ? SortState.LastNameSortDesc
                : SortState.LastNameSort;
            ViewData["MiddleNameSort"] = sortOrder == SortState.MiddleNameSort
                ? SortState.MiddleNameSortDesc
                : SortState.MiddleNameSort;
            ViewData["RoleSort"] = sortOrder == SortState.RoleSort ? SortState.RoleSortDesc : SortState.RoleSort;
            ViewData["StatusSort"] =
                sortOrder == SortState.StatusSort ? SortState.StatusSortDesc : SortState.StatusSort;

            var users = await Mediator.Send(new GetBaseUsersQuery(pageIndex, pageSize, sortOrder));
            return View(users);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> BlockUser(Guid[] selectedUsers)
        {
            await Mediator.Send(new BlockUsersCommand {UserIds = selectedUsers});
            return RedirectToAction("GetModels");
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> UnBlockUser(Guid[] selectedUsers)
        {
            await Mediator.Send(new UnBlockUsersCommand {UserIds = selectedUsers});
            return RedirectToAction("GetModels");
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> DeleteUser(Guid[] selectedUsers)
        {
            await Mediator.Send(new DeleteUsersCommand {UserIds = selectedUsers});
            return RedirectToAction("GetModels");
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> ApproveUser(string userId)
        {
            await Mediator.Send(new ApproveUserCommand {UserId = new Guid(userId)});
            return RedirectToAction("GetModels");
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> DeclineUser(Guid userId)
        {
            await Mediator.Send(new DeclineUserCommand {UserId = userId});
            return RedirectToAction("GetModels");
        }

        [HttpGet]
        public IActionResult ResetPassword(string email)
        {
            ViewBag.Email = email;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordUserCommand command)
        {
            await Mediator.Send(command);
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string email)
        {
            await Mediator.Send(new ConfirmEmailCommand {Email = email});
            return RedirectToAction("Index", "Home");
        }

    }
}