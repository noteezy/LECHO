using LECHO.Infrastructure;

namespace LECHO.Core
{
    public interface IAccountManagement
    {
        void AddNewUser(string lastName, string firstName, string middleName, int role, string login, string password);
        string GetRoleName(int roleValue);
        Users GetLecturer(int id);
        public string[] GetLecturers();
        public int GetLecturerId(string name);
        Students GetStudent(int id);
        Users GetUser(string username);
        bool Verify(string username, string password);
    }
}