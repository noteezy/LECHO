using LECHO.Infrastructure;

namespace LECHO.Core
{
    public interface ISubjectManagement
    {
        void AddSubjectToFavourite(int _UserId, int _SubjId);
        Subjects[] GetFavouriteSubjects(int _UserId, int semester);
        Favourites GetFavourite(int id);
        Faculties GetFaculty(int id);
        Subjects[] GetSubjects(int semester);
        Subjects[] GetSubjects(string title);
        Subjects[] GetSubjectsByTitle(string title, Subjects[] subjects);
    }
}