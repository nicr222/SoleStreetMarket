using AtreidesTeamProject1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace AtreidesTeamProject1.Controllers
{
    public class ContactController : Controller
    {

        private readonly string connectionString;

        public ContactController(IConfiguration configuration)
        {
            this.connectionString = configuration["ConnectionStrings:DefaultConnection"];
        }
        public IActionResult Index()
        {
            List<FeedBack> feedbacks = new List<FeedBack>();

            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {

                string sql = "SELECT TOP 5 * FROM [dbo].[FeedBack] ORDER BY DateSubmitted DESC, FeedBackID DESC"; // Fetch latest 5 feedbacks


                connection.Open();

                using (SqlDataReader reader = new SqlCommand(sql, connection).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        feedbacks.Add(new FeedBack
                        {
                            FeedBackID = reader.GetInt32(0),
                            CustomerID = reader.IsDBNull(1) ? (int?)null : reader.GetInt32(1),
                            CustomerName = reader.GetString(2),
                            Rating = reader.GetInt32(3),
                            Comment = reader.GetString(4),
                            DateSubmitted = reader.GetDateTime(5)
                        });
                    }
                }

                connection.Close();
            }

            return View(feedbacks); // Pass feedbacks to view
        }
    }
}