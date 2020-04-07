using LECHO.Infrastructure;

namespace LECHO.Core
{
    public interface ISubjectManagement
    {
        void AddSubjectToFavourite(int _UserId, int _SubjId);
        Faculties GetFaculty(int id);
        Favourites GetFavourite(int id);
        Subjects[] GetFavouriteSubjects(int _UserId, int semester);
        Subjects[] GetSubjects(int semester);
        Subjects[] GetSubjects(string title);
        Subjects[] GetSubjectsByTitle(string title, Subjects[] subjects);
    }
}