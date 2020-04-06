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
        public Faculties GetFaculty(int id)
        {
            var faculty = database.Faculties.FirstOrDefault(u => u.FacultyId == id);
            return faculty;
        }

        public Favourites GetFavourite(int id)
        {
            var subject = database.Favourites.FirstOrDefault(u => u.SubjectId == id);
            return subject;
        }
        public Subjects[] GetSubjects(int semester)
                {
                    var subject = database.Subjects.ToArray().Where(s => s.Semester == semester).Select(s => s).ToArray();
                    return subject;
                }
        public Subjects[] GetSubjects(string title) { return null; }
        public Subjects[] GetSubjectsByTitle(string title, Subjects[] subjects)
        {
            string[] titleParts = title.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var selectedSubjects = subjects.Where(r => titleParts.Any(t => r.Name.Contains(t))).Select(r => r).ToArray();
            return selectedSubjects;
        }
        public Subjects[] GetFavouriteSubjects(int _UserId, int semester)
        {
            var subjectsList = database.Favourites.ToArray().Where(u => u.UserId == _UserId).Select(u => u).ToArray();
            Subjects[] subject = new Subjects[subjectsList.Length];
            var length = 0;
            for (int i = 0; i < subjectsList.Length; i++)
            {
                subject[i] = (database.Subjects.FirstOrDefault(u => u.SubjectId == subjectsList[i].SubjectId));
                if (subject[i].Semester == semester)
                {
                    length++;
                }
            }
            Subjects[] result = new Subjects[length];
            var count = 0;
            for (int i = 0; i < subject.Length; i++)
            {
                if(subject[i].Semester == semester)
                {
                    result[count] = subject[i];
                    count++;
                }
            }
            return result;
        }
        public void AddSubjectToFavourite(int _UserId, int _SubjId)
        {
            database.Favourites.Add(new Favourites { UserId = _UserId, SubjectId = _SubjId });
            database.SaveChanges();
        }
    }
}

