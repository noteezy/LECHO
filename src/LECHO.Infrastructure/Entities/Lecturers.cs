using System;
using System.Collections.Generic;

namespace LECHO.Infrastructure
{
    public partial class Lecturers
    {
        public Lecturers()
        {
            Subjects = new HashSet<Subjects>();
        }

        public int UserId { get; set; }
        public int Faculty { get; set; }
        public string ProfileLink { get; set; }

        public virtual Faculties FacultyNavigation { get; set; }
        public virtual Users User { get; set; }
        public virtual ICollection<Subjects> Subjects { get; set; }
    }
}
