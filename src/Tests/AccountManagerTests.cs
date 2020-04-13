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
        [Fact]
        public void GetUserFirstNameTest()
        {
            using(var mock = AutoMock.GetLoose())
            {
                List<Users> users = new List<Users>
                {
                    new Users
                    {
                        Login = "testLogin",
                        FirstName = "testFirstName",
                        MiddleName ="testMiddlrName",
                        LastName = "testLastName"
                    }
                };
                IQueryable < Users > userlist= users.AsQueryable();
                var mockSet = new Mock<DbSet<Users>>();
                mockSet.As<IQueryable<Users>>().Setup(x => x.Provider).Returns(userlist.Provider);
                mockSet.As<IQueryable<Users>>().Setup(x => x.Expression).Returns(userlist.Expression);
                mockSet.As<IQueryable<Users>>().Setup(x => x.ElementType).Returns(userlist.ElementType);
                mockSet.As<IQueryable<Users>>().Setup(x => x.GetEnumerator()).Returns(userlist.GetEnumerator());
                mock.Mock<LECHOContext>().SetupGet(x => x.Users).Returns(mockSet.Object);
                var cls = mock.Create<AccountManagement>();
                var actualUser = cls.GetUser("testLogin");
                Assert.Equal(users[0].Login,actualUser.Login);
                
            }
            //AccountManagement accountManagement = new AccountManagement();
            //Users TestUser =accountManagement.GetUser("Admin");
            //Assert.Equal("this",TestUser.FirstName.ToString());
        }
        [Fact]
        public void GetUserUserNotFoundExceptionTest()
        {
            //Assert.Throws<UserNotFoundException>(()=>GetUser("UnexistingUser"));
        }
        [Fact]
        public void GetUserUserNotFoundExceptionMessageTest()
        {
            //UserNotFoundException ex = Assert.Throws<UserNotFoundException>(()=> GetUser("Unexisting user"));
           // Assert.Equal("Користувач з таким логіном не знайдений", ex.Message);
        }

    }
}
