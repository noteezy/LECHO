using LECHO.Core;
using LECHO.Infrastructure;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace LECHO.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAccountManagement accountManagement;
        private readonly ILogger<LoginController> logger;
        public LoginController(IAccountManagement _accountManagement,
                                ILogger<LoginController> _logger)
        {
            logger = _logger;
            accountManagement = _accountManagement;
        }
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
                if (accountManagement.Verify(user.Login, user.Password))
                {
                    Users u = accountManagement.GetUser(user.Login);
                    await Authenticate(u);
                    logger.LogInformation("{@User} has authorized",u);
                    return RedirectToAction("Profile", "Account");
                }
                else
                {
                    logger.LogWarning("{User} has failed to Authorize with {Password}", user.Login, user.Password);
                    return View("Error");
                }
            }
            catch (System.Exception)
            {
                logger.LogWarning("Failed attempt to Authorize with {Login} and {Password}", user.Login, user.Password);
                return View("Error");
            }  
        }
    }
}