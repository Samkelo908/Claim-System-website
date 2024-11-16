using Microsoft.AspNetCore.Mvc;
using PROG6212.PART2.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PROG6212.PART2.Controllers
{
    public class SubmitClaimController : Controller
    {
        private readonly string connectionString = "Data Source=labVMH8OX\\SQLEXPRESS;Initial Catalog=Claim;Integrated Security=True";
        private readonly IWebHostEnvironment _environment;

        public SubmitClaimController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }


        [HttpGet]
        public IActionResult Index1()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index1(string monthlyRate, string training, string miscellaneousExpenses, DateTime dateRangeStart, DateTime? endDate, string paymentMethod, string description)
        {

            if (!decimal.TryParse(monthlyRate, out decimal parsedMonthlyRate) || parsedMonthlyRate <= 0) //W3schools.com.2000.
            {
                ModelState.AddModelError("monthlyRate", "Monthly Rate must be a valid positive number.");
            }


            if (!decimal.TryParse(training, out _)) //W3schools.com.2000.
            {
                ModelState.AddModelError("training", "Training must be a valid number.");
            }

            
            if (!decimal.TryParse(miscellaneousExpenses, out _)) //W3schools.com.2000.
            {
                ModelState.AddModelError("miscellaneousExpenses", "Miscellaneous Expenses must be a valid number.");
            }


            if (endDate.HasValue && endDate < dateRangeStart)
            {
                ModelState.AddModelError("endDate", "End Date must be after the Start Date.");
            }


            if (ModelState.IsValid)
            {
                bool isSuccess = await SubmitClaimAsync(monthlyRate, training, miscellaneousExpenses, dateRangeStart, endDate, paymentMethod, description);
                if (isSuccess)
                {
                    return RedirectToAction("Index1");
                }
                else
                {
                    ModelState.AddModelError("", "An error occurred while submitting the claim. Please try again.");
                }
            }

            return View();
        }






        private async Task<bool> SubmitClaimAsync(string monthlyRate, string training, string miscellaneousExpenses, DateTime dateRangeStart, DateTime? endDate, string paymentMethod, string description)
        {
            try //W3schools.com.2000.
            {
                using (SqlConnection databaseConnection = new SqlConnection(connectionString))
                {
                    await databaseConnection.OpenAsync();

                    decimal hoursWorked = decimal.TryParse(monthlyRate, out hoursWorked) ? hoursWorked : 0;
                    hoursWorked = hoursWorked > 180 ? 180 : hoursWorked;
                    decimal hourlyRate = 500;
                    decimal totalMonthlyRate = hoursWorked * hourlyRate;

                    decimal trainingCost = decimal.TryParse(training, out trainingCost) ? trainingCost : 0;
                    decimal miscellaneousCost = decimal.TryParse(miscellaneousExpenses, out miscellaneousCost) ? miscellaneousCost : 0;

                    decimal totalAmount = totalMonthlyRate + trainingCost + miscellaneousCost;

                    
                    string sqlInsertClaim = "INSERT INTO Claim2 (MonthlyRate, Training, MiscellaneousExpenses, DateRangeStart, endDate, PaymentMethod, Description, TotalAmount) " +
                                            "VALUES (@MonthlyRate, @Training, @MiscellaneousExpenses, @DateRangeStart, @EndDate, @PaymentMethod, @Description, @TotalAmount)"; //Glynn Rudman.2024

                    using (SqlCommand command = new SqlCommand(sqlInsertClaim, databaseConnection)) //Glynn Rudman.2024
                    {
                        command.Parameters.AddWithValue("@MonthlyRate", totalMonthlyRate);
                        command.Parameters.AddWithValue("@Training", trainingCost);
                        command.Parameters.AddWithValue("@MiscellaneousExpenses", miscellaneousCost); //Glynn Rudman.2024
                        command.Parameters.AddWithValue("@DateRangeStart", dateRangeStart);
                        command.Parameters.AddWithValue("@EndDate",endDate);
                        command.Parameters.AddWithValue("@PaymentMethod", paymentMethod); //Glynn Rudman.2024
                        command.Parameters.AddWithValue("@Description", description);
                        command.Parameters.AddWithValue("@TotalAmount", totalAmount);

                        int rowsAffected = await command.ExecuteNonQueryAsync(); //Glynn Rudman.2024
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in SubmitClaimAsync: {ex.Message}");
                return false;
            }
        }





        public IActionResult ClaimSuccess()
        {
            return View();
        }

        
        private static List<ClaimModel> assignments = new List<ClaimModel>();

        public IActionResult Index()
        {
            return View(assignments);
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file, string uploaderName)
        {
            if (file != null && file.Length > 0)
            {
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "Uploads");

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var fileName = Path.GetFileName(file.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                byte[] fileBytes;
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    fileBytes = memoryStream.ToArray();
                }

                var encryptedBytes = EncryptionHelper.Encrypt(fileBytes);

                await System.IO.File.WriteAllBytesAsync(filePath, encryptedBytes);

                assignments.Add(new ClaimModel
                {
                    Id = assignments.Count + 1,
                    FileName = fileName,
                    UploaderName = uploaderName,
                    UploadDate = DateTime.Now
                });
            }
            return RedirectToAction("Index1");
        }

        
        public IActionResult OpenFile(string fileName)
        {
            var path = Path.Combine(_environment.WebRootPath, "Uploads", fileName);
            var encryptedBytes = System.IO.File.ReadAllBytes(path);
            var decryptedBytes = EncryptionHelper.Decrypt(encryptedBytes);
            return File(decryptedBytes, "application/octet-stream", fileName);
        }
    }
}

//W3schools.com.2000.SQL tutorial .Available at: https://www.w3schools.com/sql/ (Accessed: 14 October 2024).
//W3schools.com.2000. C# if ... Else. Available at: https://www.w3schools.com/cs/cs_conditions.php (Accessed:10 October 2024).
//W3schools.com. 2000. C# exceptions (try..Catch). Available at: https://www.w3schools.com/cs/cs_exceptions.php (Accessed: October 17, 2024).
//Glynn Rudman.2024.BCA2 CLDV Part 2 Workshop.May 2024.Available at: https://youtu.be/I_tiFJ-nlfE?si=NFAGPv9rajwA2cqX (Accessed:8 October 2024).
