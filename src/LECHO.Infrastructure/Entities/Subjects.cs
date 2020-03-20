using System;
using System.Collections.Generic;

namespace LECHO.Infrastructure
{
    public partial class Subjects
    {
        public Subjects()
        {
            Choices = new HashSet<Choices>();
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
        public virtual Lecturers Lecturer { get; set; }
        public virtual ICollection<Choices> Choices { get; set; }
        public virtual ICollection<Favourites> Favourites { get; set; }
    }
}
