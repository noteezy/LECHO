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


namespace LECHO.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IAccountManagement accountManagement;
        private readonly ISubjectManagement subjectManagement;
        public AccountController(IAccountManagement _accountManagement,
                                  ISubjectManagement _subjectManagement)
        {
            accountManagement = _accountManagement;
            subjectManagement = _subjectManagement;
        }
        [Authorize]
        public IActionResult Profile()
        {
            var map = new Dictionary<string, string>();
            map.Add("1", "Адмін");
            map.Add("2", "Викладач");
            map.Add("3", "Студент");

            var user = accountManagement.GetUser(User.Identity.Name);
            ViewData["FirstName"] = user.FirstName;
            ViewData["LastName"] = user.LastName;
            ViewData["MiddleName"] = user.MiddleName;
            ViewData["Role"] = map[user.Role.ToString()];
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Login");
        }
    }
}
