﻿using System;
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
        private readonly IAccountManagement accountManagement;
        private readonly ISubjectManagement subjectManagement;
        public SubjectsController(IAccountManagement _accountManagement,
                                 ISubjectManagement _subjectManagement)
        {
            accountManagement = _accountManagement;
            subjectManagement = _subjectManagement;
        }
        [Authorize]

        public ViewResult SubjectsFirstTerm(string Search)
        {
            var user = accountManagement.GetUser(User.Identity.Name);
            Subjects[] subjectsList;
            ViewData["Information"] = "";
            if (user.Role == 3)
            {
                    var student = accountManagement.GetStudent(user.UserId);
                    if (student.Course == 1)
                    {
                        subjectsList = subjectManagement.GetSubjects(3);
                    }
                    else if (student.Course == 2)
                    {
                        subjectsList = subjectManagement.GetSubjects(5);
                    }
                    else
                    {
                        subjectsList = subjectManagement.GetSubjects(1);
                        ViewData["Information"] = "Вибіркові дисципліни для вас не опубліковані.";
                    }

            }
            else
            {
                subjectsList = new List<Subjects>()
                .Concat(subjectManagement.GetSubjects(3))
                .Concat(subjectManagement.GetSubjects(5))
                .ToArray();
            }

            if (!String.IsNullOrEmpty(Search))
            {
                subjectsList = subjectManagement.GetSubjectsByTitle(Search, subjectsList);
            }
            return View(subjectsList);
        }

        public ViewResult SubjectsSecondTerm(string Search)
        {
            var user = accountManagement.GetUser(User.Identity.Name);
            Subjects[] subjectsList;
            ViewData["Information"] = "";
            if (user.Role == 3)
            {
                var student = accountManagement.GetStudent(user.UserId);
                if (student.Course == 1)
                {
                    subjectsList = subjectManagement.GetSubjects(4);
                }
                else if (student.Course == 2)
                {
                    subjectsList =  subjectManagement.GetSubjects(6);
                }
                else
                {
                    subjectsList =  subjectManagement.GetSubjects(1);
                    ViewData["Information"] = "Вибіркові дисципліни для вас не опубліковані.";
                }

            }
            else
            {
                subjectsList = new List<Subjects>()
                .Concat(subjectManagement.GetSubjects(4))
                .Concat(subjectManagement.GetSubjects(6))
                .ToArray();
            }

            if (!String.IsNullOrEmpty(Search))
            {
                subjectsList = subjectManagement.GetSubjectsByTitle(Search, subjectsList);
            }
            return View(subjectsList);
        }

        public ViewResult FavouriteFirstTerm(string Search)
        {
            var user = accountManagement.GetUser(User.Identity.Name);
            Subjects[] subjectsList;
            ViewData["Information"] = "";
            var student = accountManagement.GetStudent(user.UserId);
            if (student.Course == 1)
            {
                subjectsList = subjectManagement.GetFavouriteSubjects(user.UserId, 3);
            }
            else
            {
                subjectsList = subjectManagement.GetFavouriteSubjects(user.UserId, 5); 
            }

            if(subjectsList.Length == 0)
            {
                ViewData["Information"] = "Ви ще не обрали жодної дисципліни";
            }
            

            if (!String.IsNullOrEmpty(Search))
            {
                subjectsList = subjectManagement.GetSubjectsByTitle(Search, subjectsList);
            }
            return View(subjectsList);
        }

        public ViewResult FavouriteSecondTerm(string Search)
        {
            var user = accountManagement.GetUser(User.Identity.Name);
            Subjects[] subjectsList;
            ViewData["Information"] = "";
            var student = accountManagement.GetStudent(user.UserId);
            if (student.Course == 1)
            {
                subjectsList = subjectManagement.GetFavouriteSubjects(user.UserId, 4);
            }
            else
            {
                subjectsList = subjectManagement.GetFavouriteSubjects(user.UserId, 6);
            }
            if (subjectsList.Length == 0)
            {
                ViewData["Information"] = "Ви ще не обрали жодної дисципліни";
            }
            if (!String.IsNullOrEmpty(Search))
            {
                subjectsList = subjectManagement.GetSubjectsByTitle(Search, subjectsList);
            }
            return View(subjectsList);
        }


        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles ="3")]
        [HttpPost]
        public void AddSubjectToFavourite(int SubjId)
        {
            subjectManagement.AddSubjectToFavourite(accountManagement.GetUser(User.Identity.Name).UserId, SubjId);
        }

        [Authorize(Roles = "3")]
        [HttpPost]
        public void DeleteSubjectFromFavourite(int SubjId)
        {
            subjectManagement.DeleteSubjectFromFavourite(accountManagement.GetUser(User.Identity.Name).UserId, SubjId);
        }
    }
}
