using LECHO.Core;
using LECHO.Infrastructure;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace LECHO.Web.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Profile", "Account");
            return View();
        }
        private async Task Authenticate(Users user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString()),
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "Authorization", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
        [HttpPost]
        public async Task<IActionResult> Authorize(Users user)
        { 
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Profile", "Account");
            try
            {
                if (AccountAccess.Verify(user.Login, user.Password))
                {
                    await Authenticate(AccountAccess.GetUser(user.Login));
                    return RedirectToAction("Profile", "Account");
                }
                else
                {
                    return View("Error");
                }
            }
            catch (System.Exception)
            {
                return View("Error");
            }  
        }
    }
}