using LECHO.Infrastructure;

namespace LECHO.Core
{
    public interface IAccountManagement
    {
        string GetRoleName(int roleValue);
        Users GetLecturer(int id);
        Students GetStudent(int id);
        Users GetUser(string username);
        bool Verify(string username, string password);
    }
}