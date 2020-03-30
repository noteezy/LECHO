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
            string TestUser = "Admin";
            AccountController controller = new AccountController();
            var mock = new Mock<HttpContext>();
            mock.Setup(x => x.User.Identity.IsAuthenticated).Returns(true);
            mock.SetupGet(x => x.User.Identity.Name).Returns(TestUser);
            controller.ControllerContext.HttpContext = mock.Object;
            
            var result = controller.Profile() as ViewResult;
            
            Assert.NotNull(result.ViewData["FirstName"]);
            Assert.NotNull(result.ViewData["MiddleName"]);
        }

        [Fact]
        public void ProfileViewResultNotNullTest()
        {
            AccountController controller = new AccountController();
            var mock = new Mock<HttpContext>();
            mock.Setup(x => x.User.Identity.IsAuthenticated).Returns(true);
            mock.SetupGet(x => x.User.Identity.Name).Returns("Admin");
            controller.ControllerContext.HttpContext = mock.Object;
            
            var result = controller.Profile() as ViewResult;
            
            Assert.NotNull(result);
        }
    }
}
