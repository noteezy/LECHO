using System;
using System.Linq;
using LECHO.Infrastructure;
namespace LECHO.Core
{
    public interface IAccountManager
    {
    }
    public class AccountManagement:IAccountManager
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
