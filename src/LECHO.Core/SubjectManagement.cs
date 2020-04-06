using System;
using System.Linq;
using LECHO.Infrastructure;

namespace LECHO.Core
{
    public class SubjectManagement
    {
        private LECHOContext database;
        public SubjectManagement(LECHOContext dm)
        {
            database = dm;
        }
        public Subjects[] GetSubjects(int semester)
        {
            var subject = database.Subjects.ToArray().Where(s => s.Semester == semester).Select(s => s).ToArray();
            return subject;
        }
        public Faculties GetFaculty(int id)
        {
            var faculty = database.Faculties.FirstOrDefault(u => u.FacultyId == id);
            return faculty;
        }
        public Subjects[] GetSubjects(string title) { return null; }
        public Subjects[] GetSubjectsByTitle(string title, Subjects[] subjects)
        {
            string[] titleParts = title.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var selectedSubjects = subjects.Where(r => titleParts.Any(t => r.Name.Contains(t))).Select(r => r).ToArray();
            return selectedSubjects;
        }

        public void AddSubjectToFavourite(int _UserId, int _SubjId)
        {
            database.Favourites.Add(new Favourites { UserId = _UserId, SubjectId = _SubjId });
            database.SaveChanges();
        }
    }
}

