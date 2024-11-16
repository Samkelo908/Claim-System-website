using Microsoft.AspNetCore.Mvc;
using PROG6212.PART2.Models;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace PROG6212.PART2.Controllers
{
    public class DashBoardController : Controller
    {
        private readonly string _connectionString = "Data Source=labVMH8OX\\SQLEXPRESS;Initial Catalog=Claim;Integrated Security=True"; //Glynn Rudman.2024

        public IActionResult Index1()
        {
            var dashboardClaims = GetDashboardClaims();
            
            var pendingCount = dashboardClaims.Count(c => c.Status == "Pending");
            var acceptedCount = dashboardClaims.Count(c => c.Status == "Accepted");
            var rejectedCount = dashboardClaims.Count(c => c.Status == "Rejected");

            
            ViewBag.PendingCount = pendingCount;
            ViewBag.AcceptedCount = acceptedCount;
            ViewBag.RejectedCount = rejectedCount;

            return View(dashboardClaims);
        }

        public List<ViewClaimModel> GetDashboardClaims()
        {
            List<ViewClaimModel> dashboardClaimsList = new List<ViewClaimModel>();

            try //W3schools.com.2000
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    string sql = "SELECT ClaimID, MonthlyRate, MiscellaneousExpenses, PaymentMethod, SubmittedAt, Status FROM Claim2";
                    SqlCommand command = new SqlCommand(sql, conn); //Glynn Rudman.2024

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    foreach (DataRow row in dt.Rows) //W3schools.com.2000.
                    {
                        dashboardClaimsList.Add(new ViewClaimModel
                        {
                            ClaimID = row["ClaimID"].ToString(),
                            MonthlyRate = row["MonthlyRate"].ToString(), //Glynn Rudman.2024
                            MiscellaneousExpenses = row["MiscellaneousExpenses"].ToString(),
                            PaymentMethod = row["PaymentMethod"].ToString(),
                            SubmittedAt = Convert.ToDateTime(row["SubmittedAt"]),
                            Status = row["Status"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex) //W3schools.com.2000.
            {
                Console.WriteLine("Error fetching dashboard claims: " + ex.Message);
            }

            return dashboardClaimsList;
        }
    }
}

//W3schools.com.2000.SQL tutorial .Available at: https://www.w3schools.com/sql/ (Accessed: 14 October 2024).
//W3schools.com. 2000. C# exceptions (try..Catch). Available at: https://www.w3schools.com/cs/cs_exceptions.php (Accessed: October 17, 2024).

