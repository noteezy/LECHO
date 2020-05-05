using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LECHO.Web.Controllers
{
    public class NewsController : Controller
    {
        public IActionResult NewsPage()
        {
            return View();
        }
    }
}