using System;
using System.Collections.Generic;

namespace LECHO.Infrastructure
{
    public partial class Faculties
    {
        public Faculties()
        {
            Lecturers = new HashSet<Lecturers>();
            Students = new HashSet<Students>();
            Subjects = new HashSet<Subjects>();
        }

        public int FacultyId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public double MapLocationX { get; set; }
        public double MapLocationY { get; set; }

        public virtual ICollection<Lecturers> Lecturers { get; set; }
        public virtual ICollection<Students> Students { get; set; }
        public virtual ICollection<Subjects> Subjects { get; set; }
    }
}
