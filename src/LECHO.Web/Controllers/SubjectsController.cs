using System;
using System.Collections.Generic;
using System.Linq;
using LECHO.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LECHO.Infrastructure;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LECHO.Web.Controllers
{
    [Authorize]
    public class SubjectsController : Controller
    {
        [Authorize]
        public ViewResult Subjects()
        {
            var map = new Dictionary<string, string>();
            map.Add("1", "Адмін");
            map.Add("2", "Викладач");
            map.Add("3", "Студент");
            var user = AccountAccess.GetUser(User.Identity.Name);
            Subjects[] model;
            ViewData["Information"] = "";
            if (user.Role == 3)
            {
                var student = SubjectsAccess.GetStudent(user.UserId);
                if (student.Course == 1) 
                {
                    model = new List<Subjects>()
                    .Concat(SubjectsAccess.GetSubjects(3))
                    .Concat(SubjectsAccess.GetSubjects(4))
                    .ToArray(); 
                }
                else if (student.Course == 2) 
                {
                    model = new List<Subjects>()
                    .Concat(SubjectsAccess.GetSubjects(5))
                    .Concat(SubjectsAccess.GetSubjects(6))
                    .ToArray();
                } 
                else 
                {
                    model = SubjectsAccess.GetSubjects(1);
                    ViewData["Information"] = "Вибіркові дисципліни для вас не опубліковані.";
                }
                
            } 
            else 
            { 
                model = new List<Subjects>()
                .Concat(SubjectsAccess.GetSubjects(3))
                .Concat(SubjectsAccess.GetSubjects(4))
                .Concat(SubjectsAccess.GetSubjects(5))
                .Concat(SubjectsAccess.GetSubjects(6))
                .ToArray();
            }
            return View(model);
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
    }
}
