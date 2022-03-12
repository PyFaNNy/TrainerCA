using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Trainer.Application.Aggregates.Roles.Commands.Create;
using Trainer.Application.Aggregates.Roles.Commands.Delete;
using Trainer.Application.Aggregates.Roles.Commands.Edit;
using Trainer.Application.Aggregates.Roles.Queries.GetRole;
using Trainer.Application.Aggregates.Roles.Queries.GetRoles;
using Trainer.Domain.Entities.Role;
using Trainer.Domain.Entities.User;
using Trainer.Models;

namespace Trainer.Controllers
{
    [Route("[controller]/[action]")]
    [Authorize(Roles = "admin")]
    public class RolesController : BaseController
    {
        private readonly RoleManager<Domain.Entities.Role.Role> _roleManager;
        private readonly UserManager<User> _userManager;

        public RolesController(ILogger<RolesController> logger,RoleManager<Domain.Entities.Role.Role> roleManager, UserManager<User> userManager) : base(logger)
        {
            _roleManager = roleManager ?? throw new ArgumentNullException($"{nameof(roleManager)} is null.");
            _userManager = userManager ?? throw new ArgumentNullException($"{nameof(userManager)} is null.");
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await Mediator.Send(new GetRolesQuery());
            return View(result);
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            await Mediator.Send(new CreateRoleCommand { RoleName = name });
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid[] ids)
        {
            await Mediator.Send(new DeleteRolesCommand { Ids = ids });
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var result = await Mediator.Send(new GetRoleQuery { RoleId = id });
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Update(string name, Guid id)
        {
            await Mediator.Send(new EditRoleCommand { RoleName = name, RoleId = id });
            return RedirectToAction("Index");
        }

        //[HttpGet]
        //public async Task<IActionResult> Edit(string userId)
        //{
        //    User user = await _userManager.FindByIdAsync(userId);
        //    if (user != null)
        //    {
        //        var userRoles = await _userManager.GetRolesAsync(user);
        //        var allRoles = _roleManager.Roles.ToList();
        //        ChangeRoleViewModel model = new ChangeRoleViewModel
        //        {
        //            UserId = userId,
        //            UserEmail = user.Email,
        //            UserRoles = userRoles,
        //            AllRoles = allRoles
        //        };
        //        return View(model);
        //    }

        //    return NotFound();
        //}

        //[HttpPost]
        //public async Task<IActionResult> Edit(string userId, List<string> roles)
        //{
        //    User user = await _userManager.FindByIdAsync(userId);
        //    if (user != null)
        //    {
        //        var userRoles = await _userManager.GetRolesAsync(user);
        //        var allRoles = _roleManager.Roles.ToList();
        //        var addedRoles = roles.Except(userRoles);
        //        var removedRoles = userRoles.Except(roles);

        //        await _userManager.AddToRolesAsync(user, addedRoles);

        //        await _userManager.RemoveFromRolesAsync(user, removedRoles);

        //        return RedirectToAction("GetModels", "Users");
        //    }

        //    return NotFound();
        //}
    }
}
