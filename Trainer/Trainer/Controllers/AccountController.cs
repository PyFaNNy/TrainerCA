using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Trainer.Controllers
{
    public class AccountController : Controller
    {
        private readonly ITrainerDbContextService _contextService;
        private readonly IMapper _mapper;

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(IContextService serv, IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _contextService = serv ?? throw new ArgumentNullException($"{nameof(serv)} is null.");
            _mapper = mapper ?? throw new ArgumentNullException($"{nameof(mapper)} is null.");
            _userManager = userManager ?? throw new ArgumentNullException($"{nameof(userManager)} is null.");
            _signInManager = signInManager ?? throw new ArgumentNullException($"{nameof(signInManager)} is null.");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    MiddleName = model.MiddleName,
                    Email = model.Email,
                    UserName = model.UserName,
                    Status = StatusUser.Active
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                await _userManager.AddToRoleAsync(user, "Unknown");
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);
                    if (result.Succeeded)
                    {
                       var res = await _userManager.UpdateAsync(user);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Incorrect login and (or) password");
                    }
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetModels(SortState sortOrder = SortState.FirstNameSort)
        {
            ViewData["EmailSort"] = sortOrder == SortState.EmailSort ? SortState.EmailSortDesc : SortState.EmailSort;
            ViewData["FirstNameSort"] = sortOrder == SortState.FirstNameSort ? SortState.FirstNameSortDesc : SortState.FirstNameSort;
            ViewData["LastNameSort"] = sortOrder == SortState.LastNameSort ? SortState.LastNameSortDesc : SortState.LastNameSort;
            ViewData["MiddleNameSort"] = sortOrder == SortState.MiddleNameSort ? SortState.MiddleNameSortDesc : SortState.MiddleNameSort;

            var users = await _contextService.GetUsers(sortOrder);
            return View(users);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Block(string[] selectedUsers)
        {
            foreach (var str in selectedUsers)
            {
                User user = await _userManager.FindByNameAsync(str);
                if (user == null)
                {
                    return NotFound();
                }
                user.Status = StatusUser.Block;
                await _userManager.UpdateAsync(user);
                if (user.Status.Equals("blocked") && User.Identity.Name.Equals(user.UserName))
                {
                    await _signInManager.SignOutAsync();
                }
            }

            return RedirectToAction("AdminPanel");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> UnBlock(string[] selectedUsers)
        {
            foreach (var str in selectedUsers)
            {
                User user = await _userManager.FindByNameAsync(str);
                if (user == null)
                {
                    return NotFound();
                }
                user.Status = StatusUser.Active;
                await _userManager.UpdateAsync(user);
            }
            return RedirectToAction("AdminPanel");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Delete(string[] selectedUsers)
        {
            foreach (var str in selectedUsers)
            {
                User user = await _userManager.FindByNameAsync(str);
                if (user != null)
                {
                    if (User.Identity.Name.Equals(user.UserName))
                    {
                        await _signInManager.SignOutAsync();
                    }
                    IdentityResult result = await _userManager.DeleteAsync(user);
                }
            }
            if (_userManager.FindByNameAsync(User.Identity.Name).Status.Equals("blocked"))
            {
                return Redirect("~/Account/Logout");
            }
            return RedirectToAction("AdminPanel");
        }
    }
}
