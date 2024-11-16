using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PROG6212.PART2.Controllers;
using PROG6212.PART2.Models;
using System.Collections.Generic;

namespace PROG6212.PART2.Controllers.Tests
{
    [TestClass]
    public class AdminViewControllerTests
    {
        private AdminViewController _controller;

        [TestInitialize]
        public void Setup()
        {
            _controller = new AdminViewController();
        }

        [TestMethod]
        public void UpdateClaimStatus_ValidData_ShouldRedirectToIndex1()
        {
            
            string claimID = "1";
            string selectedStatus = "Approved"; //CODE.2024


            var result = _controller.UpdateClaimStatus(claimID, selectedStatus);

            
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            var redirectResult = (RedirectToActionResult)result;
            Assert.AreEqual("Index1", redirectResult.ActionName); //CODE.2024
        }

        [TestMethod]
        public void UpdateClaimStatus_NullOrEmptyClaimID_ShouldRedirectToIndex()
        {
            
            string claimID = null; // Medium.2023
            string selectedStatus = "Approved";

            
            var result = _controller.UpdateClaimStatus(claimID, selectedStatus);

            
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            var redirectResult = (RedirectToActionResult)result;
            Assert.AreEqual("Index", redirectResult.ActionName);
        }

        [TestMethod]
        public void UpdateClaimStatus_NullOrEmptySelectedStatus_ShouldRedirectToIndex() //CODE.2024
        {
            
            string claimID = "1";
            string selectedStatus = null; // Medium.2023


            var result = _controller.UpdateClaimStatus(claimID, selectedStatus);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            var redirectResult = (RedirectToActionResult)result;
            Assert.AreEqual("Index", redirectResult.ActionName); //CODE.2024
        }

        [TestMethod]
        public void GetAllClaims_ShouldReturnListOfClaims()
        {
           
            var result = _controller.Index1();

    
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = (ViewResult)result;
            Assert.IsInstanceOfType(viewResult.Model, typeof(List<ViewClaimModel>)); // Medium.2023
        }
    }
}


// Medium.2023. Mock testing in C# .net - Lucas Andrade, Available at: https://medium.com/@lucas.and227/mock-testing-in-c-net-22cc8412016 (Accessed: 14 October 2024).
//CODE.2024. Using Moq: A simple guide to mocking for .NET. Available at: https://www.codemag.com/Article/2305041/Using-Moq-A-Simple-Guide-to-Mocking-for-.NET (Accessed:15 October 2024).
