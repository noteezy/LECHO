using System;
using System.Collections.Generic;
using System.Linq;
using LECHO.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LECHO.Infrastructure;
using Microsoft.Extensions.Logging;
using System.Globalization;
using System.Threading.Tasks;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LECHO.Web.Controllers
{
    [Authorize]
    public class SubjectsController : Controller
    {
        private readonly IAccountManagement accountManagement;
        private readonly ISubjectManagement subjectManagement;
        private readonly ILogger<SubjectsController> logger;
        public SubjectsController(IAccountManagement _accountManagement,
                                 ISubjectManagement _subjectManagement,
                                 ILogger<SubjectsController> _logger)
        {
            accountManagement = _accountManagement;
            subjectManagement = _subjectManagement;
            logger = _logger;
        }

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
                    subjectsList = subjectManagement.GetSubjects(6);
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

        [ResponseCache(NoStore =true, Location =ResponseCacheLocation.None)]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles ="3")]
        [HttpPost]
        public void AddSubjectToFavourite(int SubjId)
        {
            Users user = accountManagement.GetUser(User.Identity.Name);
            subjectManagement.AddSubjectToFavourite(user.UserId, SubjId);
            logger.LogInformation("{@User} has added subject with id {Id} to favourites", user, SubjId);
        }

        [Authorize(Roles = "3")]
        [HttpPost]
        public void DeleteSubjectFromFavourite(int SubjId)
        {
            Users user = accountManagement.GetUser(User.Identity.Name);
            subjectManagement.DeleteSubjectFromFavourite(user.UserId, SubjId);
            logger.LogInformation("{@User} has deleted subject with id {Id} from favourites", user, SubjId);
        }

        public IActionResult SubjectInfo(int id)
        {
            try
            {
                var subject = subjectManagement.GetSingleSubjectById(id);
                ViewData["SubjectName"] = subject.Name;
                ViewData["NumberOfStudents"] = subject.NumberOfStudents;
                ViewData["MaxNumberOfStudents"] = subject.MaxNumberOfStudents;
                ViewData["Description"] = subject.Description;
                var lecturer = accountManagement.GetLecturer(subject.LecturerId);
                ViewData["LecturerName"] = lecturer.LastName + " " + lecturer.FirstName[0] + ". " + lecturer.MiddleName[0] + ".";
                var faculty = subjectManagement.GetFaculty(subject.FacultyId);
                ViewData["FacultyName"] = faculty.Name;
                ViewData["FacultyMapLocationX"] = faculty.MapLocationX.ToString("G",CultureInfo.InvariantCulture);
                ViewData["FacultyMapLocationY"] = faculty.MapLocationY.ToString("G", CultureInfo.InvariantCulture);
            }
            catch (System.Exception)
            {
                return View("Error");
            }
            return View();
        }

        [Authorize(Roles = "3")]
        public void MakeFinalSubjectChoice(int SubjId)
        {
            Users user = accountManagement.GetUser(User.Identity.Name);
            subjectManagement.MakeFinalSubjectChoice(user.UserId, SubjId);
            logger.LogInformation("{@User} has made final choice - subject with id {Id} choosen", user, SubjId);
        }

        [Authorize(Roles = "1,2")]
        public async Task<IActionResult> AddNewSubject(Subjects subject)
        {
            Users user = accountManagement.GetUser(User.Identity.Name);
            if(user.Role == 2) { subject.LecturerId = user.UserId; }
            if (!String.IsNullOrEmpty(subject.Name) && !String.IsNullOrEmpty(subject.Description) && !String.IsNullOrEmpty(subject.Faculty.Name) && !String.IsNullOrEmpty(subject.Lecturer.User.LastName) && subject.Semester != 0)
            {
                subject.FacultyId = subjectManagement.GetFacultyId(subject.Faculty.Name);
                if (user.Role != 2) { subject.LecturerId = accountManagement.GetLecturerId(subject.Lecturer.User.LastName); }
                subjectManagement.AddNewSubject(subject.Name, subject.Description, subject.FacultyId, subject.LecturerId, subject.Semester);
                return RedirectToAction("Profile", "Account");
            }
            return View();
        }

        [Authorize(Roles = "1")]
        public async Task<IActionResult> AddNewFaculty(Faculties faculty)
        {
                if (!String.IsNullOrEmpty(faculty.Name) && !String.IsNullOrEmpty(faculty.Description) && !String.IsNullOrEmpty(faculty.Address) && faculty.MapLocationX != 0.0 && faculty.MapLocationY != 0.0)
                {
                    subjectManagement.AddNewFaculty(faculty.Name, faculty.Description, faculty.Address, faculty.MapLocationX, faculty.MapLocationY);
                    return RedirectToAction("Profile", "Account");
                }
                return View(); 
        }
    }
}
