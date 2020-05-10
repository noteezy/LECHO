using LECHO.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using LECHO.Infrastructure;
using Xunit;
using Microsoft.AspNetCore.Http;
using LECHO.Core;
using Microsoft.Extensions.Logging;

namespace Tests
{
    
    public class AccountControllerTests
    {
        private Users testuser = new Users
        {
            FirstName = "TestUserFirstname",
            MiddleName = "testUserMiddlename",
            LastName = "testUserLastname",
            Role = 1
        };
        [Fact]
        public void ProfileViewResultNotNullTest()
        {
            var accmock = new Mock<IAccountManagement>();
            var sbjmock = new Mock<ISubjectManagement>();
            var logmock = new Mock<ILogger<AccountController>>();
            accmock.Setup(a => a.GetUser(It.IsAny<String>())).Returns(testuser);
            AccountController controller = new AccountController(accmock.Object, sbjmock.Object, logmock.Object);
            var mock = new Mock<HttpContext>();
            mock.SetupGet(x => x.User.Identity.IsAuthenticated).Returns(true);
            controller.ControllerContext.HttpContext = mock.Object;
            var result = controller.Profile() as ViewResult;
            Assert.NotNull(result);
        }
        [Fact]
        public void ProfileViewDataIsNotNullTests()
        {
            var accmock = new Mock<IAccountManagement>();
            var sbjmock = new Mock<ISubjectManagement>();
            var logmock = new Mock<ILogger<AccountController>>();
            accmock.Setup(a => a.GetUser(It.IsAny<string>())).Returns(testuser);
            var subjMoc = new Mock<ISubjectManagement>();
            AccountController controller = new AccountController(accmock.Object,sbjmock.Object,logmock.Object);
            var mock = new Mock<HttpContext>();
            mock.SetupGet(x => x.User.Identity.IsAuthenticated).Returns(true);
            mock.SetupGet(x => x.User.Identity.Name).Returns("sdjsl");
            controller.ControllerContext.HttpContext = mock.Object;
            Assert.NotNull((controller.Profile() as ViewResult).ViewData["FirstName"]);
            Assert.NotNull((controller.Profile() as ViewResult).ViewData["MiddleName"]);
            Assert.NotNull((controller.Profile() as ViewResult).ViewData["LastName"]);
        }
        [Fact]
        public void ProfileViewDataIsValidTests()
        {
            var accmock = new Mock<IAccountManagement>();
            var sbjmock = new Mock<ISubjectManagement>();
            var logmock = new Mock<ILogger<AccountController>>();
            accmock.Setup(a => a.GetUser(It.IsAny<string>())).Returns(testuser);
            var subjMoc = new Mock<ISubjectManagement>();
            AccountController controller = new AccountController(accmock.Object, sbjmock.Object, logmock.Object);
            var mock = new Mock<HttpContext>();
            mock.SetupGet(x => x.User.Identity.IsAuthenticated).Returns(true);
            controller.ControllerContext.HttpContext = mock.Object;
            Assert.Equal(testuser.FirstName, (controller.Profile() as ViewResult).ViewData["FirstName"]);
            Assert.Equal(testuser.MiddleName, (controller.Profile() as ViewResult).ViewData["MiddleName"]);
            Assert.Equal(testuser.LastName, (controller.Profile() as ViewResult).ViewData["LastName"]);
        }           
    }
}
