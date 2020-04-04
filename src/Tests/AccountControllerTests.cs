using LECHO.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using LECHO.Infrastructure;
using Xunit;
using Microsoft.AspNetCore.Http;
using LECHO.Core;

namespace Tests
{
    
    public class AccountControllerTests
    {
        [Fact]
        public void TestProfileViewData()
        {
            
            Users testuser = new Users { FirstName = "TestUserFirstname", MiddleName = "testcUsetMiddleame", LastName = "testUserLastname" };
            var accmock = new Mock<AccountManagement>();
            accmock.Setup(a => a.GetUser(It.IsAny<string>())).Returns(testuser);
            var subjMoc = new Mock<SubjectManagement>();
            AccountController controller = new AccountController(accmock.Object,subjMoc.Object);
            var mock = new Mock<HttpContext>();
            mock.Setup(x => x.User.Identity.IsAuthenticated).Returns(true);
            mock.Setup(x => x.User.Identity.Name).Returns(It.IsAny<string>().ToString());
            controller.ControllerContext.HttpContext = mock.Object;
 
            var result = controller.Profile() as ViewResult;
            
            Assert.NotNull(result.ViewData["FirstName"]);
            Assert.NotNull(result.ViewData["MiddleName"]);
        }

        [Fact]
        public void ProfileViewResultNotNullTest()
        {
            var accmock = new Mock<AccountManagement>();
            Users testuser = new Users { FirstName = "TestUserFirstname", MiddleName = "testcUsetMiddleame", LastName = "testUserLastname" };
            accmock.SetupGet(a => a.GetUser(It.IsAny<String>())).Returns(testuser);
            var subjMoc = new Mock<SubjectManagement>();
            AccountController controller = new AccountController(accmock.Object, subjMoc.Object);
            var mock = new Mock<HttpContext>();
            mock.Setup(x => x.User.Identity.IsAuthenticated).Returns(true);
            controller.ControllerContext.HttpContext = mock.Object;
            
            var result = controller.Profile() as ViewResult;
            
            Assert.NotNull(result);
        }
    }
}
