using LECHO.Infrastructure;

namespace LECHO.Core
{
    public interface ISubjectManagement
    {
        void AddSubjectToFavourite(int _UserId, int _SubjId);
        Faculties GetFaculty(int id);
        Subjects[] GetSubjects(int semester);
        Subjects[] GetSubjects(string title);
        Subjects[] GetSubjectsByTitle(string title, Subjects[] subjects);
    }
}