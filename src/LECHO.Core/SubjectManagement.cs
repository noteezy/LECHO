using System;
using System.Linq;
using LECHO.Infrastructure;

namespace LECHO.Core
{
    public class SubjectManagement
    {
        static private LECHOContext database = new LECHOContext();

        public static Subjects[] GetSubjects(int semester)
        {
            var subject = database.Subjects.ToArray().Where(s => s.Semester == semester).Select(s => s).ToArray();
            return subject;
        }
        public static Faculties GetFaculty(int id)
        {
            var faculty = database.Faculties.FirstOrDefault(u => u.FacultyId == id);
            return faculty;
        }

        public static Subjects[] GetSubjects(string title)
        {
            string[] titleParts = title.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var selectedSubjects = database.Subjects.ToArray().Where(r => titleParts.Any(t => r.Name.Contains(t))).Select(r => r).ToArray();
            return selectedSubjects;
        }
    }
}

