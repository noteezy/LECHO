using LECHO.Infrastructure;

namespace LECHO.Core
{
    public interface ISubjectManagement
    {
        Favourites GetFavourite(int id);
        Faculties GetFaculty(int id);
        Subjects[] GetSubjects(int semester);
        Subjects[] GetSubjects(string title);
        Subjects[] GetSubjectsByTitle(string title, Subjects[] subjects);
        Subjects[] GetFavouriteSubjects(int _UserId, int semester);
        void AddSubjectToFavourite(int _UserId, int _SubjId);
        void DeleteSubjectFromFavourite(int _UserId, int _SubjId);
    }
}
