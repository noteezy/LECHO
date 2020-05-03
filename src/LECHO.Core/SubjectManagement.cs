using System;
using System.Linq;
using LECHO.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace LECHO.Core
{
    public class SubjectManagement : ISubjectManagement
    {
        private LECHOContext database;
        public SubjectManagement(LECHOContext dm)
        {
            database = dm;
        }
        public void AddNewSubject(string name, string description, int faculty_id, int lecturer_id, int? semester)
        {
            database.Subjects.Add(new Subjects { Name = name, Description = description, FacultyId = faculty_id, LecturerId = lecturer_id, NumberOfStudents = 0, MaxNumberOfStudents = 200, Semester = semester });
            database.SaveChanges();
        }
        public void AddNewFaculty(string name, string description, string address, double map_location_x, double map_location_y)
        {

            database.Faculties.Add(new Faculties { Name = name, Description = description, Address = address, MapLocationX = map_location_x, MapLocationY = map_location_y});
            database.SaveChanges();
        }
        public Faculties GetFaculty(int id)
        {
            var faculty = database.Faculties.FirstOrDefault(u => u.FacultyId == id);
            return faculty;
        }
        public string[] GetFaculties()
        {
            var f = database.Faculties.ToArray();
            string[] faculties = new string[f.Length];
            for (int i = 0; i < f.Length; i++)
            {
                faculties[i] = f[i].Name;
            }
            return faculties;
        }

        public int GetFacultyId(string name)
        {
            var f = database.Faculties.ToArray();
            string[] titleParts = name.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            int fId = f.Where(r => name.Any(t => r.Name.Contains(t))).Select(r => r).FirstOrDefault().FacultyId;
            return fId;
        }
        public Subjects GetSingleSubjectById(int id)
        {
            var subject = database.Subjects.FirstOrDefault(s => s.SubjectId == id);
            return subject;
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
            var subjectsList = database.Subjects
                .Where(s => (database.Favourites.Where(f => f.UserId == _UserId)
                .Any(f => f.SubjectId == s.SubjectId)))
                .Where(s => s.Semester == semester)
                .Select(s => s)
                .ToArray();
            return subjectsList;
        }
        public Subjects[] GetStudentsFinalChoice(int _UserId)
        {
            var subjectsList = database.Subjects
                .Where(s => (database.Choices.Where(f => f.UserId == _UserId)
                .Any(f => f.SubjectId == s.SubjectId)))
                .OrderBy(s => s.Semester)
                .Select(s => s)
                .ToArray();
            return subjectsList;
        }
        public void AddSubjectToFavourite(int _UserId, int _SubjId)
        {
            database.Favourites.Add(new Favourites { UserId = _UserId, SubjectId = _SubjId });
            database.SaveChanges();
        }
        public void DeleteSubjectFromFavourite(int _UserId, int _SubjId)
        {
            var favouriteToDelete = new Favourites { UserId = _UserId, SubjectId = _SubjId };
            database.Remove(favouriteToDelete);
            database.SaveChanges();
        }
        public void MakeFinalSubjectChoice(int _UserId, int _SubjId)
        {
            var subjectToAdd = database.Subjects.FirstOrDefault(c => c.SubjectId == _SubjId);
            var choices = database.Choices.Where(c => c.UserId==_UserId).ToArray();
            foreach(Choices chs in choices)
            {
                var sbj = database.Subjects.FirstOrDefault(c => c.SubjectId == chs.SubjectId);
                if (sbj.Semester == subjectToAdd.Semester)
                {
                    sbj.NumberOfStudents = sbj.NumberOfStudents - 1;
                    database.Remove(chs);
                }
            }
            subjectToAdd.NumberOfStudents = subjectToAdd.NumberOfStudents + 1;
            database.Choices.Add(new Choices { UserId = _UserId, SubjectId = _SubjId });
            database.SaveChanges();
        }
    }
}