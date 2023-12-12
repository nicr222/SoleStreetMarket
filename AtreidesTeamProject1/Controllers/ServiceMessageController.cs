using AtreidesTeamProject1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace AtreidesTeamProject1.Controllers
{
    public class ServiceMessageController : Controller
    {
        private readonly string connectionString;

        // Constructor to initialize the connection string using IConfiguration
        public ServiceMessageController(IConfiguration configuration)
        {
            // Retrieving the connection string from appsettings.json
            this.connectionString = configuration["ConnectionStrings:DefaultConnection"];
        }

        // Action method to retrieve service messages from the database and display them in the view
        public IActionResult Index()
        {
            // Create a list to store service messages
            List<ServiceMessage> serviceMessages = new List<ServiceMessage>();

            // Establishing a SQL connection using the connection string
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                // SQL query to select service messages from the database in descending order of ServiceMessageID
                string sql = "SELECT * FROM [dbo].[ServiceMessage] ORDER BY ServiceMessageID DESC";

                // Open the database connection
                connection.Open();

                // Executing the SQL query and retrieving data using SqlDataReader
                using (SqlDataReader reader = new SqlCommand(sql, connection).ExecuteReader())
                {
                    // Reading each row from the result and populating ServiceMessage objects
                    while (reader.Read())
                    {
                        serviceMessages.Add(new ServiceMessage
                        {
                            // Retrieving data from the reader and assigning it to ServiceMessage properties
                            // Handling possible null values for certain columns
                            ServiceMessageID = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Email = reader.GetString(2),
                            Subject = reader.GetString(3),
                            Message = reader.GetString(4),
                            CustomerID = reader.IsDBNull(5) ? null : (int?)reader.GetInt32(5),
                            EmployeeID = reader.IsDBNull(6) ? null : (int?)reader.GetInt32(6),
                            DateSent = reader.IsDBNull(7) ? (DateTime?)null : reader.GetDateTime(7),
                            IsArchived = reader.GetBoolean(8),
                        });
                    }
                }

                // Close the database connection
                connection.Close();
            }

            // Pass the list of service messages to the view
            return View(serviceMessages);
        }

        // Action method to handle form submission and insert a new service message into the database

        [HttpPost]
        public IActionResult SubmitForm(string name, string email, string subject, string message)
        {
            // Establishing a SQL connection using the connection string
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                // SQL query to insert a new service message into the database
                string sql = "INSERT INTO [dbo].[ServiceMessage] (Name, Email, Subject, Message, DateSent) VALUES (@Name, @Email, @Subject, @Message, @DateSent)";

                // Open the database connection
                connection.Open();

                // Creating a SQL command and adding parameters to prevent SQL injection
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    // Assigning parameter values using user inputs and current date-time
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Subject", subject);
                    command.Parameters.AddWithValue("@Message", message);
                    command.Parameters.AddWithValue("@DateSent", DateTime.Now);

                    // Executing the SQL command (INSERT) to add a new service message
                    command.ExecuteNonQuery();
                }

                // Close the database connection
                connection.Close();
            }

            // Setting a temporary message for user feedback and redirecting to another action method
            TempData["MessageSent"] = "Your message has been sent. We'll get back to you shortly!";
            return RedirectToAction("Index", "Contact");
        }
    }
}

