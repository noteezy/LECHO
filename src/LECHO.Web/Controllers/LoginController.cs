using LECHO.Core;
using LECHO.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace LECHO.Web.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Autherize(Users user)
        {
            try
            {
                if (AccountAcces.Verify(user.Login, user.Password))
                {
                    ViewData["FirstName"] = AccountAcces.GetUser(user.Login).FirstName;
                    ViewData["LastName"] = AccountAcces.GetUser(user.Login).LastName;
                    
                    return View("Succesful");
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