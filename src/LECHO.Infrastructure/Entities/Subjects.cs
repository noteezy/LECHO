using System;
using System.Collections.Generic;

namespace LECHO.Infrastructure
{
    public partial class Subjects
    {
        public Subjects()
        {
            Choises = new HashSet<Choises>();
            Favourites = new HashSet<Favourites>();
        }

        public int SubjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int FacultyId { get; set; }
        public int LecturerId { get; set; }
        public int? NumberOfStudents { get; set; }
        public int MaxNumberOfStudents { get; set; }
        public int? Semester { get; set; }

        public virtual Faculties Faculty { get; set; }
        public virtual Lecturer Lecturer { get; set; }
        public virtual ICollection<Choises> Choises { get; set; }
        public virtual ICollection<Favourites> Favourites { get; set; }
    }
}
