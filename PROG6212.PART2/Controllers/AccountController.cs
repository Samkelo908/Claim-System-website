using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration; // Add this namespace for IConfiguration
using PROG6212.PART2.Models;
using Microsoft.AspNetCore.Http; // For session handling

namespace PROG6212.PART2.Controllers
{
    public class AccountController : Controller
    {
        private readonly string connectionString;

        public AccountController(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(User user)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection databaseConnection = new SqlConnection(connectionString))
                {
                    string sql = "INSERT INTO Users (name, email, password, staff) VALUES (@Name, @Email, @Password, @Staff)";
                    using (SqlCommand command = new SqlCommand(sql, databaseConnection))
                    {
                        command.Parameters.AddWithValue("@Name", user.Name);
                        command.Parameters.AddWithValue("@Email", user.Email);
                        command.Parameters.AddWithValue("@Password", user.Password); 
                        command.Parameters.AddWithValue("@Staff", user.IsStaff ? "yes" : "no");

                        databaseConnection.Open();
                        command.ExecuteNonQuery();
                    }
                }

                return RedirectToAction("Login");
            }

            return View(user);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel loginModel)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection databaseConnection = new SqlConnection(connectionString))
                {
                    string sql = "SELECT * FROM Users WHERE email = @Email AND password = @Password"; //Glynn Rudman.2024
                    using (SqlCommand command = new SqlCommand(sql, databaseConnection))
                    {
                        command.Parameters.AddWithValue("@Email", loginModel.Email);
                        command.Parameters.AddWithValue("@Password", loginModel.Password);  //Glynn Rudman.2024

                        databaseConnection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            var user = new User
                            {
                                Id = Convert.ToInt32(reader["userID"]), //Glynn Rudman.2024
                                Name = reader["name"].ToString(),
                                Email = reader["email"].ToString(),
                                IsStaff = reader["staff"].ToString() == "yes" //Glynn Rudman.2024
                            };

                            HttpContext.Session.SetString("Username", user.Name);
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Invalid email or password");
                        }
                    }
                }
            }

            return View(loginModel);
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}

//W3schools.com.2000.SQL tutorial .Available at: https://www.w3schools.com/sql/ (Accessed: 14 October 2024).
//W3schools.com.2000. C# if ... Else. Available at: https://www.w3schools.com/cs/cs_conditions.php (Accessed:10 October 2024).
//W3schools.com. 2000. C# exceptions (try..Catch). Available at: https://www.w3schools.com/cs/cs_exceptions.php (Accessed: October 17, 2024).
//Glynn Rudman.2024.BCA2 CLDV Part 2 Workshop.May 2024.Available at: https://youtu.be/I_tiFJ-nlfE?si=NFAGPv9rajwA2cqX (Accessed:8 October 2024).

