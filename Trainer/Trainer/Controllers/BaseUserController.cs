using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Trainer.Application.Aggregates.BaseUser.Commands.ApproveUser;
using Trainer.Application.Aggregates.BaseUser.Commands.BlockUser;
using Trainer.Application.Aggregates.BaseUser.Commands.DeclineUser;
using Trainer.Application.Aggregates.BaseUser.Commands.DeleteUser;
using Trainer.Application.Aggregates.BaseUser.Commands.UnBlockUser;
using Trainer.Application.Aggregates.BaseUser.Queries.GetBaseUsers;
using Trainer.Enums;

namespace Trainer.Controllers
{
    public class BaseUserController : BaseController
    {

        public BaseUserController(ILogger<BaseUserController> logger)
            : base(logger)
        {
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> GetModels(SortState sortOrder = SortState.FirstNameSort)
        {
            ViewData["EmailSort"] = sortOrder == SortState.EmailSort ? SortState.EmailSortDesc : SortState.EmailSort;
            ViewData["FirstNameSort"] = sortOrder == SortState.FirstNameSort ? SortState.FirstNameSortDesc : SortState.FirstNameSort;
            ViewData["LastNameSort"] = sortOrder == SortState.LastNameSort ? SortState.LastNameSortDesc : SortState.LastNameSort;
            ViewData["MiddleNameSort"] = sortOrder == SortState.MiddleNameSort ? SortState.MiddleNameSortDesc : SortState.MiddleNameSort;
            ViewData["RoleSort"] = sortOrder == SortState.RoleSort ? SortState.RoleSortDesc : SortState.RoleSort;
            ViewData["StatusSort"] = sortOrder == SortState.StatusSort ? SortState.StatusSortDesc : SortState.StatusSort;

            var users = await Mediator.Send(new GetBaseUsersQuery { SortOrder = sortOrder });
            return View(users);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> BlockUser(Guid[] selectedUsers)
        {
            await Mediator.Send(new BlockUsersCommand { UserIds = selectedUsers });
            return RedirectToAction("GetModels");
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> UnBlockUser(Guid[] selectedUsers)
        {
            await Mediator.Send(new UnBlockUsersCommand { UserIds = selectedUsers });
            return RedirectToAction("GetModels");
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> DeleteUser(Guid[] selectedUsers)
        {
            await Mediator.Send(new DeleteUsersCommand { UserIds = selectedUsers });
            return RedirectToAction("GetModels");
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> ApproveUser(string userId)
        {
            await Mediator.Send(new ApproveUserCommand {UserId = new Guid(userId) });
            return RedirectToAction("GetModels");
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> DeclineUser(Guid userId)
        {
            await Mediator.Send(new DeclineUserCommand { UserId = userId });
            return RedirectToAction("GetModels");
        }
    }
}
