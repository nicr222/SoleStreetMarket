using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace OrderEntrySytem.Controllers
{
    public class FeedBackController : Controller
    {
        private readonly string connectionString;

        public FeedBackController(IConfiguration configuration)
        {
            this.connectionString = configuration["ConnectionStrings:DefaultConnection"];
        }

        [HttpPost]
        public IActionResult SubmitFeedback(string name, string feedback, int rating)
        {
            // Saving feedback to database
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                string sql = "INSERT INTO [dbo].[FeedBack] (CustomerName, Rating, Comment, DateSubmitted) VALUES (@CustomerName, @Rating, @Comment, @DateSubmitted)";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@CustomerName", name);
                    command.Parameters.AddWithValue("@Rating", rating);
                    command.Parameters.AddWithValue("@Comment", feedback);
                    command.Parameters.AddWithValue("@DateSubmitted", DateTime.Now);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }

            TempData["FeedbackSent"] = "Thank you for your feedback!";
            return RedirectToAction("Index", "Contact"); // Redirect to Contact page after feedback submission
        }
    }
}

