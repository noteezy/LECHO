using System;
using System.Linq;
using LECHO.Infrastructure;
namespace LECHO.Core
{
    public class AccountManagement:IAccountManagement
    {
        public class UserNotFoundException : Exception
        {
            public UserNotFoundException() { }
            public UserNotFoundException(string message) : base(message) { }
            public UserNotFoundException(string message, Exception inner) : base(message, inner) { }
            protected UserNotFoundException(
              System.Runtime.Serialization.SerializationInfo info,
              System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
        }

        private LECHOContext database;
        public AccountManagement(LECHOContext dm)
        {
            database = dm;
        }
        public void AddNewUser(string lastName, string firstName, string middleName, int role, string login, string password)
        {

            database.Users.Add(new Users { LastName = lastName, FirstName = firstName, MiddleName = middleName, Role = role, Login = login, Password = password });
            database.SaveChanges();
        }
        public string GetRoleName(int roleValue)
        {
            switch (roleValue)
            {
                case 1:
                    return "Адмін";
                case 2:
                    return "Викладач";
                default:
                    return "Студент";
            }
        }
        public Users GetUser(string username)
        {
            var user = database.Users.FirstOrDefault(u => u.Login == username);
            if (user == null) throw new UserNotFoundException("Користувач з таким логіном не знайдений");
            return user;
        }

        public Users GetLecturer(int id)
        {
            var user = database.Users.FirstOrDefault(u => u.UserId == id);
            return user;
        }
        public string[] GetLecturers()
        {
            var user = database.Users.ToArray().Where(s => s.Role == 2).Select(s => s).ToArray();
            string[] lecturersList = new string[user.Length];
            for (int i = 0; i < user.Length; i++)
            {
                lecturersList[i] = user[i].LastName + " " + user[i].FirstName + " " + user[i].MiddleName;
            }
            return lecturersList;
        }
        public int GetLecturerId(string name)
        {
            var lecturersList = database.Users.ToArray().Where(s => s.Role == 2).Select(s => s).ToArray();
            string[] titleParts = name.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            int lId = lecturersList.Where(r => name.Any(t => (r.LastName + r.FirstName + r.MiddleName).Contains(t))).Select(r => r).FirstOrDefault().UserId;
            return lId;
        }
        public Students GetStudent(int id)
        {
            var user = database.Students.FirstOrDefault(u => u.UserId == id);
            return user;
        }
        public bool Verify(string username, string password)
        {
            return GetUser(username).Password == password;
        }
    }
}
