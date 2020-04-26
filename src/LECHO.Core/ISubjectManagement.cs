using LECHO.Infrastructure;

namespace LECHO.Core
{
    public interface ISubjectManagement
    {
        Faculties GetFaculty(int id);
        Subjects GetSingleSubjectById(int id);
        Favourites GetFavourite(int id);
        Subjects[] GetSubjects(int semester);
        Subjects[] GetSubjectsByTitle(string title, Subjects[] subjects);
        Subjects[] GetFavouriteSubjects(int _UserId, int semester);
        Subjects[] GetStudentsFinalChoice(int _UserId);
        void AddSubjectToFavourite(int _UserId, int _SubjId);
        void DeleteSubjectFromFavourite(int _UserId, int _SubjId);
        void MakeFinalSubjectChoice(int _UserId, int _SubjId);
    }
}
