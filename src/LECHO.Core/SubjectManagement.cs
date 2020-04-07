﻿using System;
using System.Linq;
using LECHO.Infrastructure;

namespace LECHO.Core
{
    public class SubjectManagement:ISubjectManagement
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
            var subjectsList = database.Subjects
                .Where(s => (database.Favourites.Where(f=>f.UserId==_UserId)
                .Any(f => f.SubjectId == s.SubjectId)))
                .Where(s=>s.Semester==semester)
                .Select(s=>s)
                .ToArray();
            return subjectsList;
        }
        public void AddSubjectToFavourite(int _UserId, int _SubjId)
        {
            database.Favourites.Add(new Favourites { UserId = _UserId, SubjectId = _SubjId });
            database.SaveChanges();
        }
    }
}

