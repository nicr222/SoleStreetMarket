using AtreidesTeamProject1.Models;
using AtreidesTeamProject1.Services;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;

namespace AtreidesTeamProject1.Controllers
{
    public class SalesController : Controller
    {
        private readonly string connectionString;

        public SalesController(IConfiguration configuration)
        {
            this.connectionString = configuration["ConnectionStrings:DefaultConnection"];
        }
        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddToSales(decimal total, int quantity)
        {
            //Creating variables.
            Sales sale = new Sales();

            //Sets the total and quantity for sale model
            sale.Total = total;
            sale.Quantity = quantity;

            //setting a value equal to the the user that is loggen in if there is a user that is logged in.
            var loggedInUser = HttpContext.Session.GetString("LoggedInUser"); //Get the username of logged-in user from the session state.

            //Tests to see if there is someone who has logged in.
            if(string.IsNullOrEmpty(loggedInUser))
            {
                //Nothing happenes if there in not a user logged in.
            }
            else
            {
                // retrieves the current user's ID from the session state
                int? userID = HttpContext.Session.GetInt32("UserID");


                if (userID == null)
                {
                    // Redirect to login if no user ID in session
                    return RedirectToAction("Login", "Account");
                }

                sale.Customer = GetCustomerByUserId(userID.Value);

            }

            return View(sale);
        }

        private Customer GetCustomerByUserId(int userID)
        {
            Customer customer = null;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[Customer] WHERE UserID = @UserID", connection);
                // Adds the userID parameter to prevent SQL injection
                cmd.Parameters.AddWithValue("@UserID", userID);
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

        [HttpPost]
        public IActionResult AddToSales(Sales sales)
        {
            //If the model is invalid it will return to the form and display an error message where needed.
            if (!ModelState.IsValid)
            {
                // Model is not valid, redisplay the form with validation errors.
                return View(sales);
            }
            //Sets the date and implements salt and hash to the card info
            sales.OrderDate = DateTime.Now;
            string salt = Salt();
            /*sales.CardNumber = Sha256Hash(sales.CardNumber, salt);*/
            string hashedCardNumber = Sha256Hash(sales.CardNumber.ToString(), salt);


            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                string sql = "INSERT INTO Sales (CustomerID, OrderDate, Quantity, Total, Name, CardNumber, Expiry, CVC, StreetAddress, City, State, Zipcode, Email, Phone)" +
                    " VALUES (@CustomerID, @OrderDate, @Quantity, @Total, @Name, @CardNumber, @Expiry, @CVC, @StreetAddress, @City, @State, @Zipcode, @Email, @Phone)";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;

                    //adding parameters
                    SqlParameter parameter = new SqlParameter
                    {
                        ParameterName = "@CustomerID",
                        Value = sales.CustomerID,
                        SqlDbType = SqlDbType.Int
                    };
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter
                    {
                        ParameterName = "@OrderDate",
                        Value = sales.OrderDate,
                        SqlDbType = SqlDbType.Date
                    };
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter
                    {
                        ParameterName = "@Quantity",
                        Value = sales.Quantity,
                        SqlDbType = SqlDbType.Int
                    };
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter
                    {
                        ParameterName = "@Total",
                        Value = sales.Total,
                        SqlDbType = SqlDbType.Decimal
                    };
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter
                    {
                        ParameterName = "@Name",
                        Value = sales.Name,
                        SqlDbType = SqlDbType.VarChar,
                        Size = 50
                    };
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter
                    {
                        ParameterName = "@CardNumber",
                        Value = hashedCardNumber,
                        SqlDbType = SqlDbType.VarChar,
                        Size = 20
                    };
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter
                    {
                        ParameterName = "@Expiry",
                        Value = sales.Expiry,
                        SqlDbType = SqlDbType.VarChar,
                        Size = 50
                    };
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter
                    {
                        ParameterName = "@CVC",
                        Value = sales.CVC,
                        SqlDbType = SqlDbType.Int
                    };
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter
                    {
                        ParameterName = "@StreetAddress",
                        Value = sales.StreetAddress,
                        SqlDbType = SqlDbType.VarChar,
                        Size = 255
                    };
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter
                    {
                        ParameterName = "@City",
                        Value = sales.City,
                        SqlDbType = SqlDbType.VarChar,
                        Size = 70
                    };
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter
                    {
                        ParameterName = "@State",
                        Value = sales.State,
                        SqlDbType = SqlDbType.VarChar,
                        Size = 70
                    };
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter
                    {
                        ParameterName = "@Zipcode",
                        Value = sales.Zipcode,
                        SqlDbType = SqlDbType.Int
                    };
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter
                    {
                        ParameterName = "@Email",
                        Value = sales.Email,
                        SqlDbType = SqlDbType.VarChar,
                        Size = 70
                    };
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter
                    {
                        ParameterName = "@Phone",
                        Value = sales.Phone,
                        SqlDbType = SqlDbType.VarChar,
                        Size = 70
                    };
                    command.Parameters.Add(parameter);


                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }

                //This will delete the items from the cart after the person has purchased their items.
                sql = "DELETE FROM Cart";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            //message stating the the purchase went through
            TempData["SuccessMessage"] = "You have successfully purchased your items!";
            return RedirectToAction("Index", "Cart");
        }
        //This is salt
        private string Salt()
        {
            byte[] saltBytes = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }

        //This is hashing
        private string Sha256Hash(string rawData, string salt)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] allBytes = Encoding.UTF8.GetBytes(rawData + salt);
                byte[] bytes = sha256Hash.ComputeHash(allBytes);
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
