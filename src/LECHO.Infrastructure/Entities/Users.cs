using System;
using System.Collections.Generic;

namespace LECHO.Infrastructure
{
    public partial class Users
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public int Role { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public virtual Lecturers Lecturers { get; set; }
        public virtual Students Students { get; set; }
    }
}
