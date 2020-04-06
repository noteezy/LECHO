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

namespace Tests
{
    public class AccountManagerTests
    {
        [Fact]
        public void GetUserFirstNameTest()
        {
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
