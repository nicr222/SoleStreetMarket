using AtreidesTeamProject1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AtreidesTeamProject1.Controllers
{
    public class ProfileController : Controller
    {
        private readonly string connectionString;

        public ProfileController(IConfiguration configuration)
        {
            this.connectionString = configuration["ConnectionStrings:DefaultConnection"];
        }

        public IActionResult Index()
        {
            // Retrieve the user ID from the session
            int? userID = HttpContext.Session.GetInt32("UserID");


            if (userID == null)
            {
                // Redirect to login if no user ID in session
                return RedirectToAction("Login", "Account");
            }

            // Retrieve customer and username information from the database
            Customer customer = GetCustomerByUserId(userID.Value);
            string username = GetUsernameByUserId(userID.Value);

            // Create a profile view model with customer data
            ProfileView model = new ProfileView
            {
                Name = customer.Name,
                Phone = customer.PhoneNum,
                Email = customer.Email,
                UserName = username,
                Address = customer.Address,
                Country = customer.Country
            };

            // Return the profile view with the populated model
            return View(model);
        }

        // Helper method to retrieve the username by user ID
        private string GetUsernameByUserId(int userID)
        {
            string username = "";
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                // Open a connection 
                connection.Open();

                // Prepare a SQL command to retrieve the username
                SqlCommand cmd = new SqlCommand(
                    "SELECT Username FROM [dbo].[User] WHERE UserID = @UserID", connection);

                // Add the userID parameter to the command
                cmd.Parameters.AddWithValue("@UserID", userID);
                SqlDataReader reader = cmd.ExecuteReader();

                // If a row is returned, set the username variable
                if (reader.HasRows)
                {
                    reader.Read();
                    username = reader.GetString(0);
                }
                connection.Close();
            }
            return username;
        }

        // POST action to edit the profile
        [HttpPost]
        public IActionResult EditProfile(ProfileView model)
        {
            // Check if the model state is valid
            if (ModelState.IsValid)
            {
                // Retrieve the user ID from the session
                int? userID = HttpContext.Session.GetInt32("UserID");

                // If no user ID exists in the session, redirect to the login page
                if (userID == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                // Update the user's profile and username in the database
                UpdateProfile(userID.Value, model);
                UpdateUsername(userID.Value, model.UserName);

                // Set a success message in TempData that can be displayed to the user
                TempData["SuccessMessage"] = "Profile updated successfully!";
            }
            else
            {
                // If the model state is not valid, set an error message
                TempData["ErrorMessage"] = "There was an issue updating your profile. Please try again.";
            }
            return RedirectToAction("Index");
        }


        private Customer GetCustomerByUserId(int userID)
        {
            // Initialize a Customer object to null
            Customer customer = null;
            // Using statement for managing the database connection
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                // Prepares an SQL command to select a customer by user ID
                SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[Customer] WHERE UserID = @UserID", connection);
                // Adds the userID parameter to prevent SQL injection
                cmd.Parameters.AddWithValue("@UserID", userID);
                // Executes the SQL command and retrieves the data
                SqlDataReader reader = cmd.ExecuteReader();

                // Checks if the query returned any rows
                if (reader.HasRows)
                {
                    // Reads the first row of the result set
                    reader.Read();
                    // Populates the customer object with data from the database
                    customer = new Customer
                    {
                        CustomerID = reader.GetInt32(0),
                        UserID = reader.GetInt32(1),
                        Name = reader.GetString(2),
                        Address = reader.IsDBNull(3) ? null : reader.GetString(3),
                        Country = reader.IsDBNull(4) ? null : reader.GetString(4),
                        PhoneNum = reader.GetString(5),
                        Email = reader.GetString(6)
                    };
                }
                connection.Close();
            }
            return customer;
        }

        private void UpdateUsername(int userID, string username)
        {
            // Using statement for managing the database connection
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                // SQL command to update the username of a user
                SqlCommand cmd = new SqlCommand(
                    "UPDATE [dbo].[User] SET Username = @Username WHERE UserID = @UserID", connection);

                // Adds parameters to the command for safety against SQL injection
                cmd.Parameters.AddWithValue("@UserID", userID);
                cmd.Parameters.AddWithValue("@Username", username);

                // Executes the SQL command without returning any data
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }


        private void UpdateProfile(int userID, ProfileView model)
        {
            // Using statement for managing the database connection
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                // SQL command to update customer details
                SqlCommand cmd = new SqlCommand(
                    "UPDATE [dbo].[Customer] SET Name = @Name, Phone = @Phone, Email = @Email, Address = @Address, Country= @Country WHERE UserID = @UserID", connection);

                // Adds parameters to the SQL command
                cmd.Parameters.AddWithValue("@UserID", userID);
                cmd.Parameters.AddWithValue("@Name", model.Name);
                cmd.Parameters.AddWithValue("@Phone", model.Phone);
                cmd.Parameters.AddWithValue("@Email", model.Email);
                cmd.Parameters.AddWithValue("@Address", model.Address);
                cmd.Parameters.AddWithValue("@Country", model.Country);

                // Executes the SQL command
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }


        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordModel model)
        {
            // Validate the model state before processing the change
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Invalid input. Please try again.";
                return RedirectToAction("Index");
            }

            // Retrieve the user ID from the session
            int? userID = HttpContext.Session.GetInt32("UserID");
            if (userID == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // Verify if the provided current password is correct
            if (!VerifyCurrentPassword(userID.Value, model.CurrentPassword))
            {
                TempData["ErrorMessage"] = "Current password is incorrect.";
                return RedirectToAction("Index");
            }

            // Ensure the new password is different from the current one
            if (model.NewPassword == model.CurrentPassword)
            {
                TempData["ErrorMessage"] = "New password cannot be the same as the current password.";
                return RedirectToAction("Index");
            }

            // Proceed to update the user's password
            UpdatePassword(userID.Value, model.NewPassword);
            // On successful update, set success message and redirect to the profile page
            TempData["SuccessMessage"] = "Password changed successfully!";
            return RedirectToAction("Index");
        }

        // Helper method to verify the current password against the stored hash
        private bool VerifyCurrentPassword(int userID, string currentPassword)
        {
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                // Prepare a SQL command to retrieve the hashed password and salt for the user
                SqlCommand cmd = new SqlCommand(
                    "SELECT Password, Salt FROM [dbo].[User] WHERE UserID = @UserID", connection);
                cmd.Parameters.AddWithValue("@UserID", userID);

                // Execute the command and read the results
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Retrieve the stored password and salt from the database
                        string storedPassword = reader.GetString(0);
                        string salt = reader.GetString(1);
                        // Hash the provided password with the stored salt
                        string hashedPassword = Sha256Hash(currentPassword, salt);

                        // Return true if the hashed password matches the stored password
                        return storedPassword == hashedPassword;
                    }
                }
            }
            return false;
        }

        // Method to update the user's password in the database
        private void UpdatePassword(int userID, string newPassword)
        {
            // Generate a new salt for the new password
            string newSalt = Salt();
            // Hash the new password with the new salt
            string hashedPassword = Sha256Hash(newPassword, newSalt);

            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                // Prepare a SQL command to update the password and salt for the user
                connection.Open();
                SqlCommand cmd = new SqlCommand(
                    "UPDATE [dbo].[User] SET Password = @Password, Salt = @Salt WHERE UserID = @UserID", connection);

                // Add parameters for the user ID, new hashed password, and new salt
                cmd.Parameters.AddWithValue("@UserID", userID);
                cmd.Parameters.AddWithValue("@Password", hashedPassword);
                cmd.Parameters.AddWithValue("@Salt", newSalt);

                // Execute the command to update the user's password
                cmd.ExecuteNonQuery();
            }
        }

        // Method to generate a new salt for hashing
        private string Salt()
        {
            // Create a byte array to hold the salt bytes
            byte[] saltBytes = new byte[32];
            // Use a secure random number generator to fill the array
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes);
            }
            // Converting the byte array to a Base64 string to make it readable and return it.
            return Convert.ToBase64String(saltBytes);
        }


        // Method to hash a string with SHA-256
        private string Sha256Hash(string rawData, string salt)
        {
            // Create a SHA256 hash object
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Combine the raw data with the salt and compute the hash
                byte[] allBytes = Encoding.UTF8.GetBytes(rawData + salt);
                byte[] bytes = sha256Hash.ComputeHash(allBytes);
                // Use a StringBuilder to create the hash string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    // Convert each byte to a two-digit hexadecimal string
                    builder.Append(bytes[i].ToString("x2"));
                }
                // Return the full hash string
                return builder.ToString();
            }
        }
    }
}
