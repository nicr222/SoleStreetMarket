
﻿using AtreidesTeamProject1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Owin.BuilderProperties;
using System.Data.SqlClient;
using System.Diagnostics.Metrics;
using System.Security.Cryptography;
using System.Text;

namespace AtreidesTeamProject1.Controllers
{
    public class AccountController : Controller
    {
		private readonly string connectionString;

		public AccountController(IConfiguration configuration)
		{
			this.connectionString = configuration["ConnectionStrings:DefaultConnection"];
		}

		[HttpGet]
		public IActionResult LogIn()
		{
			return View();
		}

		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}


		//Customer Register
		[HttpPost]
		public IActionResult Register(string Name, string Phone, string Email, string Username, string Password)
		{
            // Generate a salt for password hashing
            string salt = Salt();
            // Hash the password with the generated salt
            Password = Sha256Hash(Password, salt);

			using (SqlConnection connection = new SqlConnection(this.connectionString))
			{
				string userSql = "INSERT INTO [dbo].[User] (Username, Password, Salt) VALUES (@Username, @Password, @Salt); SELECT SCOPE_IDENTITY()";
				int userID;
                // Using SqlCommand for the user insert operation
                using (SqlCommand userCommand = new SqlCommand(userSql, connection))
				{
					userCommand.Parameters.AddWithValue("@Username", Username);
					userCommand.Parameters.AddWithValue("@Password", Password);
					userCommand.Parameters.AddWithValue("@Salt", salt);

					connection.Open();
                    // Execute the command and store the new user's ID
                    userID = Convert.ToInt32(userCommand.ExecuteScalar());
					connection.Close();
				}
                // SQL query to insert a new customer
                string customerSql = "INSERT INTO [dbo].[Customer] (UserID, Name, Phone, Email) VALUES (@UserID, @Name, @Phone, @Email)";

				using (SqlCommand customerCommand = new SqlCommand(customerSql, connection))
				{
					customerCommand.Parameters.AddWithValue("@UserID", userID);
					customerCommand.Parameters.AddWithValue("@Name", Name);
					customerCommand.Parameters.AddWithValue("@Phone", Phone);
					customerCommand.Parameters.AddWithValue("@Email", Email);

					connection.Open();
					customerCommand.ExecuteNonQuery();
					connection.Close();
				}
			}
			TempData["SuccessMessage"] = "Your account has been successfully created!";
			return RedirectToAction("LogIn");
		}

		[HttpPost]
		public IActionResult RegisterEmployee(string Name, string Phone, string Email, string Username, string Password, DateTime DateOfBirth, int DepartmentID)
		{
            // Server-side validation checks
            if (string.IsNullOrEmpty(Name))
				ModelState.AddModelError("Name", "Name is required.");
			if (string.IsNullOrEmpty(Phone) || Phone.Length < 10 || Phone.Length > 15)
				ModelState.AddModelError("Phone", "Invalid Phone Number. Please enter 10 to 15 numeric characters.");
			if (string.IsNullOrEmpty(Email) || !Email.Contains('@'))
				ModelState.AddModelError("Email", "Please enter a valid Email address.");
			if (string.IsNullOrEmpty(Username))
				ModelState.AddModelError("Username", "Username is required.");
			if (string.IsNullOrEmpty(Password) || Password.Length < 8)
				ModelState.AddModelError("Password", "Password should be at least 8 characters long.");
			if (DepartmentID == 0)
				ModelState.AddModelError("DepartmentID", "Please, select a department.");
			if (DateOfBirth != default(DateTime) && CalculateAge(DateOfBirth) < 16)
			{
				ModelState.AddModelError("DateOfBirth", "Employee must be 16 years or older.");
			}
            // If validation fails, return to the form with error messages
            if (!ModelState.IsValid)
			{
                // Gather all model errors
                List<string> errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
				ViewData["ValidationErrors"] = errors;
                // Return the view for creating an employee with the validation errors
                return View("~/Views/Portal/CreateEmployee.cshtml");
			}
            // Generate a salt and hash the password
            string salt = Salt();
			Password = Sha256Hash(Password, salt);

			using (SqlConnection connection = new SqlConnection(this.connectionString))
			{
				string userSql = "INSERT INTO [dbo].[User] (Username, Password, Salt) VALUES (@Username, @Password, @Salt); SELECT SCOPE_IDENTITY()";
				int userID;
                // Using SqlCommand for the user insert operation
                using (SqlCommand userCommand = new SqlCommand(userSql, connection))
				{
					userCommand.Parameters.AddWithValue("@Username", Username);
					userCommand.Parameters.AddWithValue("@Password", Password);
					userCommand.Parameters.AddWithValue("@Salt", salt);

					connection.Open();
                    // Execute the command and store the new user's ID
                    userID = Convert.ToInt32(userCommand.ExecuteScalar());
					connection.Close();
				}

				string Sql = "INSERT INTO [dbo].[Employee] (UserID, DepartmentID, Name, Phone, Email, DateOfBirth) VALUES (@UserID, @DepartmentID, @Name, @Phone, @Email, @DateOfBirth)";
                // Using SqlCommand for the employee insert operation
                using (SqlCommand employee = new SqlCommand(Sql, connection))
				{
					employee.Parameters.AddWithValue("@UserID", userID);
					employee.Parameters.AddWithValue("@DepartmentID", DepartmentID);
					employee.Parameters.AddWithValue("@Name", Name);
					employee.Parameters.AddWithValue("@Phone", Phone);
					employee.Parameters.AddWithValue("@Email", Email);
					employee.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);

					connection.Open();
					employee.ExecuteNonQuery();
					connection.Close();
				}
			}
			TempData["SuccessMessage"] = "Employee account has been successfully created!";
			return RedirectToAction("Index", "Portal");
		}

		// Utility method to calculate age based on date of birth
		private int CalculateAge(DateTime dateOfBirth)
		{
            // Get the current date
            var today = DateTime.Today;
            // Calculate the difference in years
            var age = today.Year - dateOfBirth.Year;
            // If the birth date this year has not occurred yet, reduce age by 1
            if (dateOfBirth.Date > today.AddYears(-age)) age--;

			return age;
		}


		[HttpPost]
		public IActionResult LogIn(string username, string password)
		{
            // Using statement for managing the database connection
            using (SqlConnection connection = new SqlConnection(this.connectionString))
			{
				connection.Open();

                // Prepares a SQL command to select user details from the database
                SqlCommand cmd = new SqlCommand(
					"SELECT UserID, Password, Salt FROM [dbo].[User] WHERE Username = @Username",
					connection);
                // Adds the username parameter to the command to prevent SQL injection
                cmd.Parameters.AddWithValue("@Username", username);

                // Executes the command and retrieves the result set
                SqlDataReader reader = cmd.ExecuteReader();

				// check if query returned any results.(if username exists in the db)
				if (reader.HasRows)
				{
                    // Read the first row from the result set
                    reader.Read();
					int userID = reader.GetInt32(0);  // Get the UserID
					string storedPasswordHash = reader["Password"].ToString(); //Retrieves hashed password from the result.
					string storedSalt = reader["Salt"].ToString(); // Retrieves salt value

                    // Check if the hashed input password matches the stored password hash
                    if (Sha256Hash(password, storedSalt) == storedPasswordHash)
					{
						HttpContext.Session.SetString("LoggedInUser", username);  // sets a session variable to remember the logged-in user's username.
						HttpContext.Session.SetInt32("UserID", userID);  //sets a session variable to remember the logged-in user’s UserID.

						// Check if the user is an employee
						reader.Close();
						SqlCommand checkEmployeeCmd = new SqlCommand(
							"SELECT COUNT(*) FROM [dbo].[Employee] WHERE UserID = @UserID",
							connection);
						checkEmployeeCmd.Parameters.AddWithValue("@UserID", userID);
						int isEmployee = (int)checkEmployeeCmd.ExecuteScalar(); //executes the SQL command and stores the result


						if (isEmployee > 0)
						{
							// User is an employee. Redirect to the portal page
							return RedirectToAction("Index", "Portal");
						}
						else
						{
							// User is a customer or other role. Continue with the default behavior
							return RedirectToAction("Index", "Home");
						}
					}
				}

				ModelState.AddModelError("", "Invalid login credentials.");
				return View();
			}
		}

		private string Salt()
		{
            //// Creating a byte array to hold 32 bytes of data.
            byte[] saltBytes = new byte[32];
			using (var rng = RandomNumberGenerator.Create()) // Use a secure random number generator to fill the array
            {
				rng.GetBytes(saltBytes);
			}
			return Convert.ToBase64String(saltBytes);// Converting the byte array to a Base64 string to make it readable and return it.
        }


        // Method to hash a string with SHA-256
        private string Sha256Hash(string rawData, string salt)
		{
			using (SHA256 sha256Hash = SHA256.Create()) // Create a SHA256 hash object
            {
                // Combine the raw data with the salt and compute the hash
                byte[] allBytes = Encoding.UTF8.GetBytes(rawData + salt);
				byte[] bytes = sha256Hash.ComputeHash(allBytes);
                // Use a StringBuilder to create the hash string. StringBuilder allows for the modification of string values without creating new string instances
                StringBuilder builder = new StringBuilder();
				for (int i = 0; i < bytes.Length; i++)
				{
                    // Converting each byte to a hexadecimal string and appending it to the StringBuilder.
                    builder.Append(bytes[i].ToString("x2"));
				}
                // Returning the hexadecimal string representation of the hash.
				return builder.ToString();
			}
		}

		public IActionResult Logout()
		{
            // Clears the current user's session data
            HttpContext.Session.Clear();

			// Redirect to Login page after logout
			return RedirectToAction("Login");
		}

	}
}
