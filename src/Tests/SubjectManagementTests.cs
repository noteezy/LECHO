using System;
using System.Collections.Generic;
using System.Text;
using LECHO.Infrastructure;

namespace Tests
{
    class SubjectManagementTests
    {
        private List<Favourites> testFavourites = new List<Favourites>
        {
            new Favourites
            {
                UserId=0,
                SubjectId=0
            },
            new Favourites
            {
                UserId=0,
                SubjectId=1
            },
            new Favourites
            {
                UserId=0,
                SubjectId=2
            },
            new Favourites
            {
                UserId=1,
                SubjectId=3
            },
            new Favourites
            {
                UserId=1,
                SubjectId=4
            },
            new Favourites
            {
                UserId=2,
                SubjectId=5
            },
            new Favourites
            {
                UserId=1,
                SubjectId=6
            }
        };
        private List<Subjects> testSubjects = new List<Subjects>
        {
            new Subjects
            {
                SubjectId=0,
                Semester=5
            },
            new Subjects
            {
                SubjectId=1,
                Semester=5
            },
            new Subjects
            {
                SubjectId=2,
                Semester=6
            },
            new Subjects
            {
                SubjectId=3,
                Semester=6
            },
            new Subjects
            {
                SubjectId=4,
                Semester=3
            },
            new Subjects
            {
                SubjectId=5,
                Semester=3
            },
            new Subjects
            {
                SubjectId=6,
                Semester=4
            },
            new Subjects
            {
                SubjectId=7,
                Semester=4
            },
            new Subjects
            {
                SubjectId=8,
                Semester=6
            },
            new Subjects
            {
                SubjectId=9,
                Semester=3
            }
        };
    }
}