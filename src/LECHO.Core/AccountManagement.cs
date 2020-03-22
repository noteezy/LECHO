using System;
using System.Linq;
using LECHO.Infrastructure;
namespace LECHO.Core
{
    public class AccountManagement
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

        static private LECHOContext database = new LECHOContext();

        public static Users GetUser(string username)
        {
            var user = database.Users.FirstOrDefault(u => u.Login == username);
            if (user == null) throw new UserNotFoundException("Користувач з таким іменем не знайдений");
            return user;
        }
        public static Users GetLecturer(int id)
        {
            var user = database.Users.FirstOrDefault(u => u.UserId == id);
            return user;
        }
        public static Students GetStudent(int id)
        {
            var user = database.Students.FirstOrDefault(u => u.UserId == id);
            return user;
        }
        public static bool Verify(string username, string password)
        {
            return GetUser(username).Password == password;
        }
    }
}
