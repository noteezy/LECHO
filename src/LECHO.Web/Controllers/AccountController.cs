using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LECHO.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        [Authorize(Roles ="1")]
        public IActionResult Profile()
        {
            return View();
        }
    }
}