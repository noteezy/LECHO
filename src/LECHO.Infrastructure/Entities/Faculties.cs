using System;
using System.Collections.Generic;

namespace LECHO.Infrastructure
{
    public partial class Faculties
    {
        public Faculties()
        {
            Lecturer = new HashSet<Lecturer>();
            Student = new HashSet<Student>();
            Subjects = new HashSet<Subjects>();
        }

        public int FacultyId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Adress { get; set; }
        public double MapLocationX { get; set; }
        public double MapLocationY { get; set; }

        public virtual ICollection<Lecturer> Lecturer { get; set; }
        public virtual ICollection<Student> Student { get; set; }
        public virtual ICollection<Subjects> Subjects { get; set; }
    }
}
