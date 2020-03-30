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

        public ViewResult SubjectsFirstTerm()
        {
            var map = new Dictionary<string, string>();
            map.Add("1", "Адмін");
            map.Add("2", "Викладач");
            map.Add("3", "Студент");
            var user = AccountManagement.GetUser(User.Identity.Name);
            Subjects[] subjectsList;
            ViewData["Information"] = "";
            if (user.Role == 3)
            {
                var student = AccountManagement.GetStudent(user.UserId);
                if (student.Course == 1) 
                {
                    subjectsList = SubjectManagement.GetSubjects(3);
                }
                else if (student.Course == 2) 
                {
                    subjectsList = SubjectManagement.GetSubjects(5);

                } 
                else 
                {
                    subjectsList = SubjectManagement.GetSubjects(1);
                    ViewData["Information"] = "Вибіркові дисципліни для вас не опубліковані.";
                }
                
            } 
            else 
            {
                subjectsList = new List<Subjects>()
                .Concat(SubjectManagement.GetSubjects(3))
                .Concat(SubjectManagement.GetSubjects(5))
                .ToArray();
            }
            return View(subjectsList);
        }

        public ViewResult SubjectsSecondTerm()
        {
            var map = new Dictionary<string, string>();
            map.Add("1", "Адмін");
            map.Add("2", "Викладач");
            map.Add("3", "Студент");
            var user = AccountManagement.GetUser(User.Identity.Name);
            Subjects[] subjectsList;
            ViewData["Information"] = "";
            if (user.Role == 3)
            {
                var student = AccountManagement.GetStudent(user.UserId);
                if (student.Course == 1)
                {
                    subjectsList = SubjectManagement.GetSubjects(4);
                }
                else if (student.Course == 2)
                {
                    subjectsList = SubjectManagement.GetSubjects(6);
                }
                else
                {
                    subjectsList = SubjectManagement.GetSubjects(1);
                    ViewData["Information"] = "Вибіркові дисципліни для вас не опубліковані.";
                }

            }
            else
            {
                subjectsList = new List<Subjects>()
                .Concat(SubjectManagement.GetSubjects(4))
                .Concat(SubjectManagement.GetSubjects(6))
                .ToArray();
            }
            return View(subjectsList);
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
    }
}
