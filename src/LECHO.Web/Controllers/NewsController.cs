using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace LECHO.Web.Controllers
{
    public class NewsController : Controller
    {
        [Authorize(Roles = "1")]
        public IActionResult NewsPage()
        {
            return View();
        }
    }
}