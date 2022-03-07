using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Trainer.Application.Aggregates.Users.Commands.BlockUser;
using Trainer.Application.Aggregates.Users.Commands.DeleteUser;
using Trainer.Application.Aggregates.Users.Commands.SignInUser;
using Trainer.Application.Aggregates.Users.Commands.UnBlockUser;
using Trainer.Application.Aggregates.Users.Queries.GetUsers;
using Trainer.Enums;

namespace Trainer.Controllers
{
    public class UsersController : BaseController
    {
        public UsersController(ILogger<UsersController> logger)
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

            var users = await Mediator.Send(new GetUsersQuery { SortOrder = sortOrder });
            return View(users);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Block(Guid[] selectedUsers)
        {
            await Mediator.Send(new BlockUsersCommand { UserIds = selectedUsers });
            return RedirectToAction("GetModels");
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> UnBlock(Guid[] selectedUsers)
        {
            await Mediator.Send(new UnBlockUsersCommand { UserIds = selectedUsers });
            return RedirectToAction("GetModels");
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Delete(Guid[] selectedUsers)
        {
            await Mediator.Send(new DeleteUsersCommand { UserIds = selectedUsers });
            return RedirectToAction("GetModels");
        }
    }
}
