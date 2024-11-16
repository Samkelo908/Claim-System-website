using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.AspNetCore.Mvc;
using PROG6212.PART2.Controllers;
using PROG6212.PART2.Models;
using System.Collections.Generic;

namespace PROG6212.PART2Tests.Controllers
{
    [TestClass]
    public class AdminViewControllerTests
    {
        private AdminViewController _controller;

        [TestInitialize]
        public void Setup()
        {
            _controller = new AdminViewController(); //CODE.2024.
        }

        [TestMethod]
        public void Index1_ReturnsViewResult_WithClaimsList()
        {
            var result = _controller.Index1() as ViewResult;

            Assert.IsNotNull(result);
            var model = result.Model as List<ViewClaimModel>; //CODE.2024.
            Assert.IsNotNull(model);
        }

        [TestMethod]
        public void UpdateClaimStatus_WithValidInput_RedirectsToIndex1()
        {
            var claimID = "1";
            var selectedStatus = "Approved";

            var result = _controller.UpdateClaimStatus(claimID, selectedStatus) as RedirectToActionResult; //CODE.2024.

            Assert.IsNotNull(result);
            Assert.AreEqual("Index1", result.ActionName);
        }

        [TestMethod]
        public void UpdateClaimStatus_WithInvalidInput_ReturnsRedirectToIndex()
        {
            var claimID = "";
            var selectedStatus = "";

            var result = _controller.UpdateClaimStatus(claimID, selectedStatus) as RedirectToActionResult; // Medium.2023

            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ActionName); // Medium.2023
        }
    }
}

// Medium.2023. Mock testing in C# .net - Lucas Andrade, Available at: https://medium.com/@lucas.and227/mock-testing-in-c-net-22cc8412016 (Accessed: 14 October 2024).
//CODE.2024. Using Moq: A simple guide to mocking for .NET. Available at: https://www.codemag.com/Article/2305041/Using-Moq-A-Simple-Guide-to-Mocking-for-.NET (Accessed:15 October 2024).

