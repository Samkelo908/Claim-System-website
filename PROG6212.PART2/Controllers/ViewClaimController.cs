using Microsoft.AspNetCore.Mvc;
using PROG6212.PART2.Models;
using System.Data;
using System.Data.SqlClient;

namespace PROG6212.PART2.Controllers
{
    public class ViewClaimController : Controller
    {
        private readonly string _connectionString = "Data Source=labVMH8OX\\SQLEXPRESS;Initial Catalog=Claim;Integrated Security=True";

        public IActionResult Index1()
        {
            var claims = GetUserClaims();
            return View(claims);
        }

        private List<ViewClaimModel> GetUserClaims()
        {
            List<ViewClaimModel> claimsList = new List<ViewClaimModel>();

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    string sql = "SELECT ClaimID, MonthlyRate, MiscellaneousExpenses, PaymentMethod, SubmittedAt, Status FROM Claim2";
                    SqlCommand command = new SqlCommand(sql, conn);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    foreach (DataRow row in dt.Rows)
                    {
                        claimsList.Add(new ViewClaimModel
                        {
                            ClaimID = row["ClaimID"].ToString(),
                            MonthlyRate = row["MonthlyRate"].ToString(),
                            MiscellaneousExpenses = row["MiscellaneousExpenses"].ToString(),
                            PaymentMethod = row["PaymentMethod"].ToString(),
                            SubmittedAt = Convert.ToDateTime(row["SubmittedAt"]),
                            Status = row["Status"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching claims: " + ex.Message);
            }

            return claimsList;
        }
    }
}


//W3schools.com.2000.SQL tutorial .Available at: https://www.w3schools.com/sql/ (Accessed: 14 October 2024).
//W3schools.com.2000. C# if ... Else. Available at: https://www.w3schools.com/cs/cs_conditions.php (Accessed:10 October 2024).
//W3schools.com. 2000. C# exceptions (try..Catch). Available at: https://www.w3schools.com/cs/cs_exceptions.php (Accessed: October 17, 2024).
//Glynn Rudman.2024.BCA2 CLDV Part 2 Workshop.May 2024.Available at: https://youtu.be/I_tiFJ-nlfE?si=NFAGPv9rajwA2cqX (Accessed:8 October 2024).
