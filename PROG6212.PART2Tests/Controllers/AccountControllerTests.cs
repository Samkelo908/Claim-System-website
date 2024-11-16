using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.Extensions.Configuration;
using PROG6212.PART2.Controllers;
using PROG6212.PART2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace PROG6212.PART2.Controllers.Tests
{
    [TestClass()]
    public class AccountControllerTests
    {
        private AccountController _controller;

        [TestInitialize]
        public void Setup()
        {
            
            var inMemorySettings = new Dictionary<string, string>
            {
                {"ConnectionStrings:DefaultConnection", "FakeConnectionString"} //CODE.2024.
            };

            IConfiguration configuration = new ConfigurationBuilder() //CODE.2024.
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            
            var mockHttpContext = new DefaultHttpContext(); //CODE.2024.
            mockHttpContext.Session = new Mock<ISession>().Object;

            _controller = new AccountController(configuration)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext
                }
            };
        }

        [TestMethod]
        public void SignUp_Get_ReturnsView()
        {
            
            var result = _controller.SignUp() as ViewResult;

            
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Login_Get_ReturnsView() // Medium.2023.
        {
           
            var result = _controller.Login() as ViewResult; // Medium.2023.


            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Logout_ClearsSessionAndRedirectsToLogin()// Medium.2023.
        {
            
            var result = _controller.Logout() as RedirectToActionResult;

           
            Assert.IsNotNull(result);
            Assert.AreEqual("Login", result.ActionName);
        }
    }
}
// Medium.2023. Mock testing in C# .net - Lucas Andrade, Available at: https://medium.com/@lucas.and227/mock-testing-in-c-net-22cc8412016 (Accessed: 14 October 2024).
//CODE.2024. Using Moq: A simple guide to mocking for .NET. Available at: https://www.codemag.com/Article/2305041/Using-Moq-A-Simple-Guide-to-Mocking-for-.NET (Accessed:15 October 2024).

