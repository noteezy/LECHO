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
        private List<Users> TestUsers = new List<Users>
                {
                    new Users
                    {
                        Login = "testLogin",
                        FirstName = "testFirstName",
                        MiddleName ="testMiddlrName",
                        LastName = "testLastName",
                        Password="Qqwerty",
                        Role=1,
                        UserId=1
                    },
                    new Users
                    {
                        Login = "testLogin1",
                        FirstName = "testFirstName1",
                        MiddleName ="testMiddlrName1",
                        LastName = "testLastName1",
                        Password="Qqwerty",
                        Role=1,
                        UserId=2
                    },
                    new Users
                    {
                        Login = "testLogin2",
                        FirstName = "testFirstName2",
                        MiddleName ="testMiddlrName2",
                        LastName = "testLastName2",
                        Password="Qqwerty",
                        Role=1,
                        UserId=3
                    },
                    new Users
                    {
                        Login = "testLogin3",
                        FirstName = "testFirstName3",
                        MiddleName ="testMiddlrName3",
                        LastName = "testLastName3",
                        Password="Qqwerty",
                        Role=1,
                        UserId=4
                    },
                    new Users
                    {
                        Login = "testLogin4",
                        FirstName = "testFirstName4",
                        MiddleName ="testMiddlrName4",
                        LastName = "testLastName4",
                        Password="Qqwerty",
                        Role=1,
                        UserId=5
                    },
                    new Users
                    {
                        Login = "testLogin5",
                        FirstName = "testFirstName5",
                        MiddleName ="testMiddlrName5",
                        LastName = "testLastName5",
                        Password="Qqwerty",
                        Role=1,
                        UserId=6
                    }
                };
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
                Name="Test Subject Number One 1",
                SubjectId=0,
                Semester=5
            },
            new Subjects
            {
                Name="Test Subject Number Two 2",
                SubjectId=1,
                Semester=5
            },
            new Subjects
            {
                Name="Test Subject Number three 3",
                SubjectId=2,
                Semester=6
            },
            new Subjects
            {
                Name="Some Other Test Subject",
                SubjectId=3,
                Semester=6
            },
            new Subjects
            {
                Name="I on4 ? 423I",
                SubjectId=4,
                Semester=3
            },
            new Subjects
            {
                Name="Another Subject",
                SubjectId=5,
                Semester=3
            },
            new Subjects
            {
                Name="Realy uniq name",
                SubjectId=6,
                Semester=4
            },
            new Subjects
            {
                Name="Any string here",
                SubjectId=7,
                Semester=4
            },
            new Subjects
            {
                Name="Some Name",
                SubjectId=8,
                Semester=6
            },
            new Subjects
            {
                Name="Unnecessary Name",
                SubjectId=9,
                Semester=3
            }
        };
        private List<Faculties> testFaculties = new List<Faculties>
        {
            new Faculties
            {
                //this name so wierd special for tests
                Name="First Test Faculty Number One 1",
                FacultyId=1
            },
            new Faculties
            {
                Name="Faculty2",
                FacultyId=2
            },
            new Faculties
            {
                Name="Faculty3",
                FacultyId=3
            },
            new Faculties
            {
                Name="Faculty4",
                FacultyId=4
            },
            new Faculties
            {
                Name="Faculty5",
                FacultyId=5
            },
            new Faculties
            {
                Name="Faculty6",
                FacultyId=6
            },
            new Faculties
            {
                Name="Faculty7",
                FacultyId=7
            },
        };
        private List<Choices> testchoices = new List<Choices>
        {
            new Choices
            {
                SubjectId=0,
                UserId=1
            },
            new Choices
            {
                SubjectId=0,
                UserId=2
            },
            new Choices
            {
                SubjectId=1,
                UserId=2
            },
            new Choices
            {
                SubjectId=0,
                UserId=3
            },
            new Choices
            {
                SubjectId=0,
                UserId=4
            },
            new Choices
            {
                SubjectId=1,
                UserId=4
            },
            new Choices
            {
                SubjectId=0,
                UserId=5
            },
            new Choices
            {
                SubjectId=1,
                UserId=6
            },
            new Choices
            {
                SubjectId=2,
                UserId=6
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
        [Fact]
        public void getFaculties_LengthTest() 
        {
            using(var mock = AutoMock.GetLoose())
            {
                IQueryable<Faculties> facultiesList = testFaculties.AsQueryable();
                var facultiesMockSet = new Mock<DbSet<Faculties>>();
                facultiesMockSet.As<IQueryable<Faculties>>().Setup(x => x.GetEnumerator()).Returns(facultiesList.GetEnumerator());
                mock.Mock<LECHOContext>().SetupGet(x => x.Faculties).Returns(facultiesMockSet.Object);
                var SubjectManagementMock = mock.Create<SubjectManagement>();
                string[] actualFacultyNames = SubjectManagementMock.GetFaculties();
                Assert.Equal(testFaculties.Count, actualFacultyNames.Length);
            }
        }
        [Fact]
        public void getFaculties_NamesTest()
        {
            using (var mock = AutoMock.GetLoose())
            {
                IQueryable<Faculties> facultiesList = testFaculties.AsQueryable();
                var facultiesMockSet = new Mock<DbSet<Faculties>>();
                facultiesMockSet.As<IQueryable<Faculties>>().Setup(x => x.GetEnumerator()).Returns(facultiesList.GetEnumerator());
                mock.Mock<LECHOContext>().SetupGet(x => x.Faculties).Returns(facultiesMockSet.Object);
                var SubjectManagementMock = mock.Create<SubjectManagement>();
                string[] actualFacultyNames = SubjectManagementMock.GetFaculties();
                for (int i = 0; i < testFaculties.Count; ++i)
                {
                    Assert.Equal(testFaculties[i].Name, actualFacultyNames[i]);
                }
            }
        }
        [Theory]
        [InlineData("One",1)]
        [InlineData("2", 2)]
        [InlineData("One 1", 1)]
        [InlineData("First Test Faculty Number One 1",1)]
        [InlineData("One and some odds", 1)]
        [InlineData("First Test Faculty Number One 1 and some odds", 1)]
        public void getFacultyIdTest(string testName, int expectedId)
        {
            using (var mock = AutoMock.GetLoose())
            {
                IQueryable<Faculties> facultiesList = testFaculties.AsQueryable();
                var facultiesMockSet = new Mock<DbSet<Faculties>>();
                facultiesMockSet.As<IQueryable<Faculties>>().Setup(x => x.GetEnumerator()).Returns(facultiesList.GetEnumerator());
                mock.Mock<LECHOContext>().SetupGet(x => x.Faculties).Returns(facultiesMockSet.Object);
                var SubjectManagementMock = mock.Create<SubjectManagement>();
                int actualId = SubjectManagementMock.GetFacultyId(testName);
                Assert.Equal(expectedId, actualId);
            }
        }
        [Theory]
        [InlineData("One",1)]
        [InlineData("Number", 3)]
        [InlineData("Test", 4)]
        [InlineData("Subject", 5)]
        [InlineData("Subject and some odds",5)]
        [InlineData("Test Subject Number One 1 and some odds", 5)]
        public void getSubjectsByTitleTest(string testNameString,int expectedLength)
        {
            using (var mock = AutoMock.GetLoose())
            {
                var SubjectManagementMock = mock.Create<SubjectManagement>();
                Subjects[] testSubjectsArray = testSubjects.ToArray();
                Subjects[] actualSubjects = SubjectManagementMock.GetSubjectsByTitle(testNameString, testSubjectsArray);
                Assert.Equal(expectedLength, actualSubjects.Length);
            }
        }
        [Theory]
        [InlineData(2,2)]
        [InlineData(3, 1)]
        [InlineData(5, 0)]
        public void getStudentsFinalChoiseLengthTest(int testUserId,int expectedLength)
        {
            using (var mock = AutoMock.GetLoose())
            {
                IQueryable<Choices> schoisesList = testchoices.AsQueryable();
                var choisesMockSet = new Mock<DbSet<Choices>>();
                choisesMockSet.As<IQueryable<Choices>>().Setup(x => x.Provider).Returns(schoisesList.Provider);
                choisesMockSet.As<IQueryable<Choices>>().Setup(x => x.Expression).Returns(schoisesList.Expression);
                IQueryable<Subjects> subjectList = testSubjects.AsQueryable();
                var SubjectsMockSet = new Mock<DbSet<Subjects>>();
                SubjectsMockSet.As<IQueryable<Subjects>>().Setup(x => x.Provider).Returns(subjectList.Provider);
                SubjectsMockSet.As<IQueryable<Subjects>>().Setup(x => x.Expression).Returns(subjectList.Expression);
                mock.Mock<LECHOContext>().SetupGet(x => x.Choices).Returns(choisesMockSet.Object);
                mock.Mock<LECHOContext>().SetupGet(x => x.Subjects).Returns(SubjectsMockSet.Object);
                var SubjectManagementMock = mock.Create<SubjectManagement>();
                Subjects[] actualChoises = SubjectManagementMock.GetStudentsFinalChoice(testUserId);
                Assert.Equal(expectedLength, actualChoises.Length);
            }
        }
        [Fact]
        public void getStudentsFinalChoiseActualValueTest()
        {
            using (var mock = AutoMock.GetLoose())
            {
                IQueryable<Choices> schoisesList = testchoices.AsQueryable();
                var choisesMockSet = new Mock<DbSet<Choices>>();
                choisesMockSet.As<IQueryable<Choices>>().Setup(x => x.Provider).Returns(schoisesList.Provider);
                choisesMockSet.As<IQueryable<Choices>>().Setup(x => x.Expression).Returns(schoisesList.Expression);
                IQueryable<Subjects> subjectList = testSubjects.AsQueryable();
                var SubjectsMockSet = new Mock<DbSet<Subjects>>();
                SubjectsMockSet.As<IQueryable<Subjects>>().Setup(x => x.Provider).Returns(subjectList.Provider);
                SubjectsMockSet.As<IQueryable<Subjects>>().Setup(x => x.Expression).Returns(subjectList.Expression);
                mock.Mock<LECHOContext>().SetupGet(x => x.Choices).Returns(choisesMockSet.Object);
                mock.Mock<LECHOContext>().SetupGet(x => x.Subjects).Returns(SubjectsMockSet.Object);
                var SubjectManagementMock = mock.Create<SubjectManagement>();
                int testUserId = 4;
                Subjects[] expectedSubjects = new Subjects[2] { testSubjects.ToArray()[0], testSubjects.ToArray()[1]};
                Subjects[] actualSubjects = SubjectManagementMock.GetStudentsFinalChoice(testUserId);
                Assert.Equal(expectedSubjects, actualSubjects);
            }
        }
    }
}