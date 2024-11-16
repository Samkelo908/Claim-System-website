using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PROG6212.PART2.Controllers;
using PROG6212.PART2.Models;
using System;
using System.Collections.Generic;

namespace PROG6212.PART2.Tests.Controllers
{
    [TestClass]
    public class ViewClaimControllerTests
    {
        private Mock<IClaimRepository> _mockClaimRepository;
        private ViewClaimController _controller;

        [TestInitialize]
        public void Setup()
        {
            
            _mockClaimRepository = new Mock<IClaimRepository>();

            
            _controller = new ViewClaimController(_mockClaimRepository.Object);
        }

        [TestMethod]
        public void Index1_ReturnsViewWithClaims()
        {

            var expectedClaims = new List<ViewClaimModel>
            {
                new ViewClaimModel
                {
                    MonthlyRate = "5000",
                    MiscellaneousExpenses = "200",
                    PaymentMethod = "Cash",
                    SubmittedAt = DateTime.Now,
                    Status = "Approved"
                },
                new ViewClaimModel
                {
                    MonthlyRate = "6000",
                    MiscellaneousExpenses = "300",
                    PaymentMethod = "Card",
                    SubmittedAt = DateTime.Now,
                    Status = "Pending"
                }
            };

           
            _mockClaimRepository.Setup(repo => repo.GetUserClaims()).Returns(expectedClaims);


            var result = _controller.Index1() as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(List<ViewClaimModel>));
            var model = result.Model as List<ViewClaimModel>;
            Assert.AreEqual(2, model.Count);  
        }
    }
}
// Medium.2023. Mock testing in C# .net - Lucas Andrade, Available at: https://medium.com/@lucas.and227/mock-testing-in-c-net-22cc8412016 (Accessed: 14 October 2024).
//CODE.2024. Using Moq: A simple guide to mocking for .NET. Available at: https://www.codemag.com/Article/2305041/Using-Moq-A-Simple-Guide-to-Mocking-for-.NET (Accessed:15 October 2024).
