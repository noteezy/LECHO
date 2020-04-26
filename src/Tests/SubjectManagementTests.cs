using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac.Extras.Moq;
using LECHO.Core;
using LECHO.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Tests
{
    public class SubjectManagementTests
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
        [Fact]
        public void getFavouriteSubgectTest()
        {
            using(var mock = AutoMock.GetLoose())
            {
                IQueryable<Favourites> favouriteList = testFavourites.AsQueryable();
                var favouritesMockSet = new Mock<DbSet<Favourites>>();
                favouritesMockSet.As<IQueryable<Favourites>>().Setup(x => x.Provider).Returns(favouriteList.Provider);
                favouritesMockSet.As<IQueryable<Favourites>>().Setup(x => x.Expression).Returns(favouriteList.Expression);
                IQueryable<Subjects> subjectList = testSubjects.AsQueryable();
                var SubjectsMockSet = new Mock<DbSet<Subjects>>();
                SubjectsMockSet.As<IQueryable<Subjects>>().Setup(x => x.Provider).Returns(subjectList.Provider);
                SubjectsMockSet.As<IQueryable<Subjects>>().Setup(x => x.Expression).Returns(subjectList.Expression);
                mock.Mock<LECHOContext>().SetupGet(x => x.Favourites).Returns(favouritesMockSet.Object);
                mock.Mock<LECHOContext>().SetupGet(x => x.Subjects).Returns(SubjectsMockSet.Object);
                var SubjectManagementMock = mock.Create<SubjectManagement>();
                int userId = 0;
                int semester = 3;
                Subjects[] expectedSubjects = new Subjects[] { };
                Subjects[] actualSubjects = SubjectManagementMock.GetFavouriteSubjects(userId, semester);
                Assert.Equal(expectedSubjects, actualSubjects);   
            }
        }
    }
}