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
using Serilog;
using Microsoft.Extensions.Logging;

namespace LECHO.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IAccountManagement accountManagement;
        private readonly ISubjectManagement subjectManagement;
        private readonly ILogger<AccountController> logger;
        public AccountController(IAccountManagement _accountManagement, ISubjectManagement _subjectManagement,
                                 ILogger<AccountController> _logger)
        {
            accountManagement = _accountManagement;
            subjectManagement = _subjectManagement;
            logger = _logger;
        }
        [Authorize]
        public IActionResult Profile()
        {
            var user = accountManagement.GetUser(User.Identity.Name);
            var subjects = subjectManagement.GetStudentsFinalChoice(user.UserId);
            ViewData["FirstName"] = user.FirstName;
            ViewData["LastName"] = user.LastName;
            ViewData["MiddleName"] = user.MiddleName;
            ViewData["Role"] = accountManagement.GetRoleName(user.Role);
            return View(subjects);
        }
        public async Task<IActionResult> Logout()
        {
            logger.LogInformation("{User} logged out", User.Identity.Name);
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Login");
        }
    }
}
