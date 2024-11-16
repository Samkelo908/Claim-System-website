using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PROG6212.PART2.Controllers;
using PROG6212.PART2.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace PROG6212.PART2.Controllers.Tests
{
    [TestClass]
    public class SubmitClaimControllerTests
    {
        private SubmitClaimController _controller;
        private Mock<IWebHostEnvironment> _mockEnvironment;

        [TestInitialize]
        public void Setup()
        {
            _mockEnvironment = new Mock<IWebHostEnvironment>();
            _mockEnvironment.Setup(m => m.WebRootPath).Returns("wwwroot"); //CODE.2024
            _controller = new SubmitClaimController(_mockEnvironment.Object);
        }

        [TestMethod]
        public void Index1_ShouldReturnView()
        {
            // Act
            var result = _controller.Index1();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        //[TestMethod]
        //public async Task SubmitClaimAsync_ValidInput_ShouldRedirectToIndex1()
        //{
            
        //    string monthlyRate = "160"; 
        //    string training = "1000"; 
        //    string miscellaneousExpenses = "500"; 
        //    DateTime dateRangeStart = DateTime.Now;
        //    DateTime? endDate = DateTime.Now.AddDays(1); 
        //    string paymentMethod = "Cash";
        //    string description = "Valid claim description";


        //    var result = await _controller.Index1(monthlyRate, training, miscellaneousExpenses, dateRangeStart, endDate, paymentMethod, description);


        //    Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        //    var redirectResult = (RedirectToActionResult)result;
        //    Assert.AreEqual("Index1", redirectResult.ActionName);
        //}

        [TestMethod]
        public async Task SubmitClaimAsync_InvalidInput_ShouldReturnViewWithError()
        {
            
            string monthlyRate = "InvalidRate";
            string training = "1000";
            string miscellaneousExpenses = "500";
            DateTime dateRangeStart = DateTime.Now;
            DateTime? endDate = DateTime.Now;
            string paymentMethod = "Cash";
            string description = "Claim description"; // Medium.2023


            var result = await _controller.Index1(monthlyRate, training, miscellaneousExpenses, dateRangeStart, endDate, paymentMethod, description);

           
            var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult, "Expected the method to return a ViewResult when input is invalid.");
            Assert.IsFalse(viewResult.ViewData.ModelState.IsValid, "ModelState should be invalid for incorrect input.");
            Assert.IsTrue(viewResult.ViewData.ModelState.ContainsKey("monthlyRate"), "Expected model state to contain errors for the monthlyRate field.");

            var monthlyRateError = viewResult.ViewData.ModelState["monthlyRate"];
            Assert.IsNotNull(monthlyRateError, "Expected error for the monthlyRate field.");
            Assert.IsTrue(monthlyRateError.Errors.Any(), "Expected error message for the monthlyRate field.");
        }

        [TestMethod]
        public async Task Upload_ValidFile_ShouldRedirectToIndex1() //CODE.2024
        {
            
            var mockFile = new Mock<IFormFile>();
            var content = "File content";
            var fileName = "testfile.txt"; // Medium.2023
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;

            mockFile.Setup(f => f.OpenReadStream()).Returns(ms);
            mockFile.Setup(f => f.FileName).Returns(fileName);
            mockFile.Setup(f => f.Length).Returns(ms.Length);

            string uploaderName = "TestUploader";

            
            var result = await _controller.Upload(mockFile.Object, uploaderName);

            
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            var redirectResult = (RedirectToActionResult)result; // Medium.2023
            Assert.AreEqual("Index1", redirectResult.ActionName);
        }

        [TestMethod]
        public async Task Upload_NoFile_ShouldRedirectToIndex1()
        {
            
            IFormFile file = null;
            string uploaderName = "TestUploader";

            
            var result = await _controller.Upload(file, uploaderName);

           
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult)); // Medium.2023
            var redirectResult = (RedirectToActionResult)result;
            Assert.AreEqual("Index1", redirectResult.ActionName);
        }
    }
}

// Medium.2023. Mock testing in C# .net - Lucas Andrade, Available at: https://medium.com/@lucas.and227/mock-testing-in-c-net-22cc8412016 (Accessed: 14 October 2024).
//CODE.2024. Using Moq: A simple guide to mocking for .NET. Available at: https://www.codemag.com/Article/2305041/Using-Moq-A-Simple-Guide-to-Mocking-for-.NET (Accessed:15 October 2024).
