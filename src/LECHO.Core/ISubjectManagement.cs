using LECHO.Infrastructure;

namespace LECHO.Core
{
    public interface ISubjectManagement
    {
        void AddNewSubject(string name, string description, int faculty_id, int lecturer_id, int? semester);
        Faculties GetFaculty(int id);
        public string[] GetFaculties();
        public int GetFacultyId(string name);
        Subjects GetSingleSubjectById(int id);
        Favourites GetFavourite(int id);
        Subjects[] GetSubjects(int semester);
        Subjects[] GetSubjectsByTitle(string title, Subjects[] subjects);
        Subjects[] GetFavouriteSubjects(int _UserId, int semester);
        Subjects[] GetStudentsFinalChoice(int _UserId); 
        void AddNewFaculty(string name, string description, string address, double map_location_x, double map_location_y);
        void AddSubjectToFavourite(int _UserId, int _SubjId);
        void DeleteSubjectFromFavourite(int _UserId, int _SubjId);
        void MakeFinalSubjectChoice(int _UserId, int _SubjId);
    }
}
