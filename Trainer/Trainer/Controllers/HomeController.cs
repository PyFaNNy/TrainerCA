using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Trainer.Models;

namespace Trainer.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }

        [HttpPost]
        public IActionResult SetTheme(string theme, string returnUrl)
        {
            CookieOptions cookies = new CookieOptions();
            cookies.Expires = DateTime.Now.AddDays(1);

            Response.Cookies.Append("theme", theme, cookies);

            return LocalRedirect(returnUrl);
        }
    }
}
