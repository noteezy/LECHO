using Microsoft.EntityFrameworkCore;
using Moq;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using LECHO.Infrastructure;
using LECHO.Core;
using static LECHO.Core.AccountManagement;
using Autofac.Extras.Moq;

namespace Tests
{
    public class AccountManagerTests
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
        //OddTest
        //ToDelete
        [Theory]
        [InlineData("testLogin", "testFirstName","testMiddlrName", "testLastName")]
        [InlineData("testLogin1", "testFirstName1", "testMiddlrName1", "testLastName1")]
        public void GetUserTest(string actualLogin, string expectedFirstName,string expectedMiddleName,string expectedLastName)
        {
            using(var mock = AutoMock.GetLoose())
            {
                IQueryable < Users > userlist= TestUsers.AsQueryable();
                var mockSet = new Mock<DbSet<Users>>();
                mockSet.As<IQueryable<Users>>().Setup(x => x.Provider).Returns(userlist.Provider);
                mockSet.As<IQueryable<Users>>().Setup(x => x.Expression).Returns(userlist.Expression);
                //mockSet.As<IQueryable<Users>>().Setup(x => x.ElementType).Returns(userlist.ElementType);
               // mockSet.As<IQueryable<Users>>().Setup(x => x.GetEnumerator()).Returns(userlist.GetEnumerator());
                mock.Mock<LECHOContext>().SetupGet(x => x.Users).Returns(mockSet.Object);
                var AccountManagementMock = mock.Create<AccountManagement>();
                var actualUser = AccountManagementMock.GetUser(actualLogin);
                Assert.Equal(expectedFirstName, actualUser.FirstName);
                Assert.Equal(expectedMiddleName, actualUser.MiddleName);
                Assert.Equal(expectedLastName,actualUser.LastName);
            }
        }
        [Fact]
        public void GetUserUserNotFoundExceptionTest()
        {
            using (var mock = AutoMock.GetLoose())
            {
                IQueryable<Users> userlist = TestUsers.AsQueryable();
                var mockSet = new Mock<DbSet<Users>>();
                mockSet.As<IQueryable<Users>>().Setup(x => x.Provider).Returns(userlist.Provider);
                mockSet.As<IQueryable<Users>>().Setup(x => x.Expression).Returns(userlist.Expression);
                mock.Mock<LECHOContext>().SetupGet(x => x.Users).Returns(mockSet.Object);
                var AccountManagementMock = mock.Create<AccountManagement>();
                string unexistingLogin = "unexistingLogin";
                Action tryToFindUser = ()=> AccountManagementMock.GetUser(unexistingLogin);
                Assert.Throws<UserNotFoundException>(tryToFindUser);
            }
        }
        [Theory]
        [InlineData("testLogin", "Qqwerty",true)]
        [InlineData("testLogin", "WrongPassword", false)]
        public void VerifyTest(string Login,string Password,bool expectedVerificationValue)
        {
            using (var mock = AutoMock.GetLoose())
            {
                IQueryable<Users> userlist = TestUsers.AsQueryable();
                var mockSet = new Mock<DbSet<Users>>();
                mockSet.As<IQueryable<Users>>().Setup(x => x.Provider).Returns(userlist.Provider);
                mockSet.As<IQueryable<Users>>().Setup(x => x.Expression).Returns(userlist.Expression);
                mock.Mock<LECHOContext>().SetupGet(x => x.Users).Returns(mockSet.Object);
                var AccountManagementMock = mock.Create<AccountManagement>();
                bool actualVerificationValue = AccountManagementMock.Verify(Login, Password);
                Assert.Equal(expectedVerificationValue, actualVerificationValue);
            }
        }
    }
}
