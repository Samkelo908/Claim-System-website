using Microsoft.AspNetCore.Mvc;
using PROG6212.PART2.Models;
using System.Data;
using System.Data.SqlClient;

namespace PROG6212.PART2.Controllers
{
    public class AdminViewController : Controller
    {
        private readonly string _connectionString = "Data Source=labVMH8OX\\SQLEXPRESS;Initial Catalog=Claim;Integrated Security=True";

        public IActionResult Index1()
        {
            var claims = GetAllClaims();
            return View(claims);
        }

      
     
        [HttpPost]
        public IActionResult UpdateClaimStatus(string claimID, string SelectedStatus)
        {
            Console.WriteLine($"ClaimID: {claimID}, SelectedStatus: {SelectedStatus}");

            if (string.IsNullOrEmpty(claimID) || string.IsNullOrEmpty(SelectedStatus)) //W3schools.com.2000.
            {
                Console.WriteLine("Error: ClaimID or SelectedStatus is null or empty.");
                return RedirectToAction("Index"); 
            }

            UpdateClaim(claimID, SelectedStatus);
            return RedirectToAction("Index1");
        }



        private List<ViewClaimModel> GetAllClaims()
        {
            List<ViewClaimModel> claimsList = new List<ViewClaimModel>();

            try //W3schools.com.2000.
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))//Glynn Rudman.2024
                {
                    conn.Open();
                    string sql = "SELECT ClaimID, MonthlyRate, MiscellaneousExpenses, PaymentMethod, SubmittedAt, Status, DateRangeStart FROM Claim2";
                    SqlCommand command = new SqlCommand(sql, conn);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    foreach (DataRow row in dt.Rows) //W3schools.com.2000.
                    {
                        claimsList.Add(new ViewClaimModel //Glynn Rudman.2024
                        {
                            ClaimID = row["ClaimID"].ToString(), 
                            MonthlyRate = row["MonthlyRate"].ToString(), //Glynn Rudman.2024
                            MiscellaneousExpenses = row["MiscellaneousExpenses"].ToString(),
                            DateRangeStart = Convert.ToDateTime(row["DateRangeStart"]),
                            PaymentMethod = row["PaymentMethod"].ToString(), //Glynn Rudman.2024
                            SubmittedAt = Convert.ToDateTime(row["SubmittedAt"]),
                            Status = row["Status"].ToString() //Glynn Rudman.2024
                        });
                    }
                }
            }
            catch (Exception ex) //W3schools.com.2000.
            {
                Console.WriteLine("Error fetching claims: " + ex.Message);
            }

            return claimsList;
        }

        private void UpdateClaim(string claimID, string newStatus)
        {
            try //W3schools.com.2000.
            {
                using (SqlConnection conn = new SqlConnection(_connectionString)) //Glynn Rudman.2024
                {
                    conn.Open();
                    string sql = "UPDATE Claim2 SET Status = @Status WHERE ClaimID = @ClaimID"; //Glynn Rudman.2024
                    SqlCommand command = new SqlCommand(sql, conn);
                    command.Parameters.AddWithValue("@Status", newStatus); //Glynn Rudman.2024
                    command.Parameters.AddWithValue("@ClaimID", claimID);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex) //W3schools.com.2000.
            {
                Console.WriteLine("Error updating claim: " + ex.Message);
            }
        }

    }
}


//W3schools.com.2000.SQL tutorial .Available at: https://www.w3schools.com/sql/ (Accessed: 14 October 2024).
//W3schools.com.2000. C# if ... Else. Available at: https://www.w3schools.com/cs/cs_conditions.php (Accessed:10 October 2024).
//W3schools.com. 2000. C# exceptions (try..Catch). Available at: https://www.w3schools.com/cs/cs_exceptions.php (Accessed: October 17, 2024).
//Glynn Rudman.2024.BCA2 CLDV Part 2 Workshop.May 2024.Available at: https://youtu.be/I_tiFJ-nlfE?si=NFAGPv9rajwA2cqX (Accessed:8 October 2024).
