using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LECHO.Core;
using LECHO.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LECHO.Infrastructure;
using Microsoft.Extensions.Logging;

namespace LECHO.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IAccountManagement accountManagement;
        private readonly ILogger<AccountController> logger;
        public AccountController(IAccountManagement _accountManagement,
                                 ILogger<AccountController> _logger)
        {
            accountManagement = _accountManagement;
            logger = _logger;
        }
        [Authorize]
        public IActionResult Profile()
        {
            var user = accountManagement.GetUser(User.Identity.Name);
            ViewData["FirstName"] = user.FirstName;
            ViewData["LastName"] = user.LastName;
            ViewData["MiddleName"] = user.MiddleName;
            ViewData["Role"] = accountManagement.GetRoleName(user.Role);
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            logger.LogInformation("{User} logged out", User.Identity.Name);
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Login");
        }
    }
}
