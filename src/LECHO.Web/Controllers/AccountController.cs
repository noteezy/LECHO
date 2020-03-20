using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LECHO.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LECHO.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        [Authorize]
        public IActionResult Profile()
        {
            var map = new Dictionary<string, string>();
            map.Add("1", "Адмін");
            map.Add("2", "Викладач");
            map.Add("3", "Студент");

            var user = AccountAccess.GetUser(User.Identity.Name);
            ViewData["FirstName"] = user.FirstName;
            ViewData["LastName"] = user.LastName;
            ViewData["MiddleName"] = user.MiddleName;
            ViewData["Role"] = map[user.Role.ToString()];
            return View();
        }
        //[HttpPost]
        //public IActionResult Logout()
        //{
        //    HttpContext.Session.Clear();
        //    return RedirectToAction("Login", "Login");
        //}
    }
}