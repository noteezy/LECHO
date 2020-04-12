﻿using LECHO.Infrastructure;

namespace LECHO.Core
{
    public interface ISubjectManagement
    {
        Faculties GetFaculty(int id);
        Subjects GetSingleSubjectById(int id);
        Favourites GetFavourite(int id);
        Subjects[] GetSubjects(int semester);
        Subjects[] GetSubjects(string title);
        Subjects[] GetSubjectsByTitle(string title, Subjects[] subjects);
        Subjects[] GetFavouriteSubjects(int _UserId, int semester);
        void AddSubjectToFavourite(int _UserId, int _SubjId);
        void DeleteSubjectFromFavourite(int _UserId, int _SubjId);
    }
}
