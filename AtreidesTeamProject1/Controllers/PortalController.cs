using AtreidesTeamProject1.Models;
using AtreidesTeamProject1.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Reflection.Metadata;

namespace AtreidesTeamProject1.Controllers
{

    
	public class PortalController : Controller
    {
        private readonly ILogger<PortalController> _logger;
        private readonly string connectionString;

        public PortalController(ILogger<PortalController> logger, IConfiguration configuration)
        {
            _logger = logger;
            Configuration = configuration;
            this.connectionString = configuration["ConnectionStrings:DefaultConnection"];
        }

        public IConfiguration Configuration { get; }


		public IActionResult Index()
		{
			// Assuming you have the user's ID from the current session
			int userId = HttpContext.Session.GetInt32("UserID") ?? 0; // Default to 0 if not found

			using (SqlConnection connection = new SqlConnection(this.connectionString))
			{
				connection.Open();

				// Check if the user is an employee
				SqlCommand checkEmployeeCmd = new SqlCommand(
					"SELECT COUNT(*) FROM [dbo].[Employee] WHERE UserID = @UserID",
					connection);
				checkEmployeeCmd.Parameters.AddWithValue("@UserID", userId);
				int isEmployee = (int)checkEmployeeCmd.ExecuteScalar(); // executes the SQL command and stores the result

				if (isEmployee > 0)
				{
					// User is an employee. Allow access to the view.

					// Replace this logic with your data retrieval logic.
					List<Department> departments = GetDepartmentList();
					List<Products> products = GetProductList();
					List<AboutUs> employee = GetEmployeeList();
					List<ServiceMessage> serviceMessages = GetServiceMessageList();
					List<FeedBack> feedBacks = GetFeedBackList();

					AllModels allModels = new AllModels();

					allModels.Department = departments;
					allModels.Products = products;
					allModels.Employees = employee;
					allModels.ServiceMessages = serviceMessages;
					allModels.FeedBack = feedBacks;

                    List<Sales> salesList = new List<Sales>();
                    

                        //SQL Query command that selects everything from the sales table 
                        string sql = "Select * From Sales";
                        SqlCommand command = new SqlCommand(sql, connection);

                        //Runs the sql command
                        using (SqlDataReader dataReader = command.ExecuteReader())
                        {
                            //Running through all of the data that was just grabbed
                            while (dataReader.Read())
                            {
                                //creating a new sale for each sale that was collected.
                                Sales sales = new Sales();

                                //Assigning values from the table to a variable that inherits from the sales model
                                sales.Total = Convert.ToInt32(dataReader["Total"]);
                                sales.OrderDate = Convert.ToDateTime(dataReader["OrderDate"]);
                                sales.Quantity = Convert.ToInt32(dataReader["Quantity"]);
                                sales.Name = Convert.ToString(dataReader["Name"]);
                                salesList.Add(sales);
                            }
                        
                        connection.Close();
                    }
                    allModels.Sales = salesList;

                    return View(allModels);
				}
				else
				{
					// User is not an employee. Redirect to an error view or handle accordingly.
					return RedirectToAction("Index", "Error"); // Example: Show an error view
				}
			}
		}


		/// <summary>
		/// Create Employee Veiw Get
		/// </summary>
		/// <returns>Returns the page</returns>
		public IActionResult CreateEmployee()
        {
            
           
                // Assuming you have the user's ID from the current session
                int userId = HttpContext.Session.GetInt32("UserID") ?? 0; // Default to 0 if not found

                using (SqlConnection connection = new SqlConnection(this.connectionString))
                {
                    connection.Open();

                    // Check if the user is an employee
                    SqlCommand checkEmployeeCmd = new SqlCommand(
                        "SELECT COUNT(*) FROM [dbo].[Employee] WHERE UserID = @UserID",
                        connection);
                    checkEmployeeCmd.Parameters.AddWithValue("@UserID", userId);
                    int isEmployee = (int)checkEmployeeCmd.ExecuteScalar(); // executes the SQL command and stores the result

                    if (isEmployee > 0)
                    {
                        // User is an employee. Show the CreateEmployee view.
                        return View();
                    }
                else
                {
                    // User is not signed in or not an employee. Redirect to an error view or handle accordingly.
                    return RedirectToAction("Index", "Error"); // Example: Show an error view
                }
            }
             

            
        }

        /// <summary>
        /// Create Employee View Post
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>Posts the created employee.</returns>
        [HttpPost]
        public IActionResult CreateEmployee(AboutUs employee)
        {
            if (!ModelState.IsValid)
            {
                return View(employee);
            }
            else
            {
                TempData["SuccessMessage"] = "The employee has been successfully created!";
            }

            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                string sql = "INSERT INTO [Employee] (Name, DepartmentID, DateOfBirth) VALUES (@Name, @DepartmentID, @DateOfBirth)";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;

                    //adding parameters
                    SqlParameter parameter = new SqlParameter
                    {
                        ParameterName = "@Name",
                        Value = employee.Name,
                        SqlDbType = SqlDbType.VarChar,
                        Size = 255
                    };
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter
                    {
                        ParameterName = "@DepartmentID",
                        Value = employee.DepartmentID,
                        SqlDbType = SqlDbType.Int,
                        
                    };
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter
                    {
                        ParameterName = "@DateOfBirth",
                        Value = employee.DateOfBirth,
                        SqlDbType = SqlDbType.Date
                        
                    };
                    command.Parameters.Add(parameter);


                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return RedirectToAction("Index", employee);
        }

        public IActionResult CreateDepartment()
        {

            // Assuming you have the user's ID from the current session
            int userId = HttpContext.Session.GetInt32("UserID") ?? 0; // Default to 0 if not found

            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();

                // Check if the user is an employee
                SqlCommand checkEmployeeCmd = new SqlCommand(
                    "SELECT COUNT(*) FROM [dbo].[Employee] WHERE UserID = @UserID",
                    connection);
                checkEmployeeCmd.Parameters.AddWithValue("@UserID", userId);
                int isEmployee = (int)checkEmployeeCmd.ExecuteScalar(); // executes the SQL command and stores the result

                if (isEmployee > 0)
                {
                    // User is an employee. Show the CreateEmployee view.
                    return View();
                }
                else
                {
                    // User is not signed in or not an employee. Redirect to an error view or handle accordingly.
                    return RedirectToAction("Index", "Error"); // Example: Show an error view
                }
            }
        }

        [HttpPost]
        public IActionResult CreateDepartment(Department department)
        {
            if (!ModelState.IsValid)
            {
                return View(department);
            }
            else
            {
                TempData["SuccessMessage"] = "The department has been successfully created!";
            }

            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                string sql = "INSERT INTO Department (DepartmentName, DepartmentDesc) VALUES (@DepartmentName, @DepartmentDesc)";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;

                    //adding parameters
                    SqlParameter parameter = new SqlParameter
                    {
                        ParameterName = "@DepartmentName",
                        Value = department.DepartmentName,
                        SqlDbType = SqlDbType.VarChar,
                        Size = 150
                    };
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter
                    {
                        ParameterName = "@DepartmentDesc",
                        Value = department.DepartmentDesc,
                        SqlDbType = SqlDbType.VarChar,
                        Size = 255

                    };
                    command.Parameters.Add(parameter);

                    


                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return RedirectToAction("Index", department);
        }

        public IActionResult CreateProduct()
        {

            // Assuming you have the user's ID from the current session
            int userId = HttpContext.Session.GetInt32("UserID") ?? 0; // Default to 0 if not found

            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();

                // Check if the user is an employee
                SqlCommand checkEmployeeCmd = new SqlCommand(
                    "SELECT COUNT(*) FROM [dbo].[Employee] WHERE UserID = @UserID",
                    connection);
                checkEmployeeCmd.Parameters.AddWithValue("@UserID", userId);
                int isEmployee = (int)checkEmployeeCmd.ExecuteScalar(); // executes the SQL command and stores the result

                if (isEmployee > 0)
                {
                    // User is an employee. Show the CreateEmployee view.
                    return View();
                }
                else
                {
                    // User is not signed in or not an employee. Redirect to an error view or handle accordingly.
                    return RedirectToAction("Index", "Error"); // Example: Show an error view
                }
            }
        }

        [HttpPost]
        public IActionResult CreateProduct(Products product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
                TempData["SuccessMessage"] = "The product has been successfully created!";
            }

            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                string sql = "INSERT INTO Product (TypeID, Name, Description, DepartmentID, Price, URL) VALUES (@TypeID, @Name, @Description, @DepartmentID, @Price, @URL)";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;

                    //adding parameters
                    SqlParameter parameter = new SqlParameter
                    {
                        ParameterName = "@TypeID",
                        Value = product.TypeID,
                        SqlDbType = SqlDbType.Int
                    };
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter
                    {
                        ParameterName = "@Name",
                        Value = product.Name,
                        SqlDbType = SqlDbType.VarChar,
                        Size = 50
                    };
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter
                    {
                        ParameterName = "@Description",
                        Value = product.Description,
                        SqlDbType = SqlDbType.VarChar,
                        Size = 255
                    };
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter
                    {
                        ParameterName = "@DepartmentID",
                        Value = product.DepartmentID,
                        SqlDbType = SqlDbType.Int
                    };
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter
                    {
                        ParameterName = "@Price",
                        Value = product.Price,
                        SqlDbType = SqlDbType.Decimal
                    };
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter
                    {
                        ParameterName = "@URL",
                        Value = product.Url,
                        SqlDbType = SqlDbType.VarChar,
                        Size = 255
                    };
                    command.Parameters.Add(parameter);


                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return RedirectToAction("Index", product);
        }

        public IActionResult CreateItem()
        {

            // Assuming you have the user's ID from the current session
            int userId = HttpContext.Session.GetInt32("UserID") ?? 0; // Default to 0 if not found

            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();

                // Check if the user is an employee
                SqlCommand checkEmployeeCmd = new SqlCommand(
                    "SELECT COUNT(*) FROM [dbo].[Employee] WHERE UserID = @UserID",
                    connection);
                checkEmployeeCmd.Parameters.AddWithValue("@UserID", userId);
                int isEmployee = (int)checkEmployeeCmd.ExecuteScalar(); // executes the SQL command and stores the result

                if (isEmployee > 0)
                {
                    // User is an employee. Show the CreateEmployee view.
                    return View();
                }
                else
                {
                    // User is not signed in or not an employee. Redirect to an error view or handle accordingly.
                    return RedirectToAction("Index", "Error"); // Example: Show an error view
                }
            }
        }

        public IActionResult UpdateD(int id)
        {

            Department department = new Department();

            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                string sql = $"SELECT * FROM Department WHERE DepartmentId = '{id}'";
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();

                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        department.DepartmentId = Convert.ToInt32(dataReader["DepartmentId"]);
                        department.DepartmentName = Convert.ToString(dataReader["DepartmentName"]);
                        department.DepartmentDesc = Convert.ToString(dataReader["DepartmentDesc"]);
                        department.IsArchived = Convert.ToBoolean(dataReader["IsArchived"]);
                    }
                }
                connection.Close();
            }

            return View(department);
        }

        [HttpPost]
        public IActionResult UpdateD(Department department, int id)
        {


            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                string sql = $"UPDATE Department SET DepartmentName = '{department.DepartmentName}', DepartmentDesc = '{department.DepartmentDesc}' WHERE DepartmentID = '{id}'";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;

                    //adding parameters
                    SqlParameter parameter = new SqlParameter
                    {
                        ParameterName = "@DepartmentName",
                        Value = department.DepartmentName,
                        SqlDbType = SqlDbType.VarChar,
                        Size = 150
                    };
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter
                    {
                        ParameterName = "@DepartmentDesc",
                        Value = department.DepartmentDesc,
                        SqlDbType = SqlDbType.VarChar,
                        Size = 255

                    };
                    command.Parameters.Add(parameter);




                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return RedirectToAction("Index", department);
        }

        public IActionResult UpdateE(int id)
        {
            AboutUs employee = new AboutUs();
            var department = new ListService(Configuration).GetDepartmentList();
            
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                string sql = $"Select * From Employee Where EmployeeID='{id}'";
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();

                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        employee.EmployeeID = Convert.ToInt32(dataReader["EmployeeID"]);
                        employee.Name = Convert.ToString(dataReader["Name"]);
                        employee.DepartmentID = Convert.ToInt32(dataReader["DepartmentID"]);
                        employee.Department = department.Where(department => department.DepartmentId == employee.DepartmentID).FirstOrDefault();
                        employee.DateOfBirth = DateTime.Parse(dataReader["DateOfBirth"].ToString());
                        employee.IsArchived = Convert.ToBoolean(dataReader["IsArchived"]);
                    }
                }
                connection.Close();
            }
            return View(employee);
        }

        [HttpPost]
        public IActionResult UpdateE(AboutUs employee, int id)
        {
            if (!ModelState.IsValid)
            {
                return View(employee);
            }
            else
            {
                TempData["SuccessMessage"] = "The employee has been successfully updated!";
            }

            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                string sql = $"UPDATE [Employee] SET Name = '{employee.Name}', DepartmentID = '{employee.DepartmentID}', DateOfBirth = '{employee.DateOfBirth}' WHERE EmployeeID = '{id}'";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;

                    // Adding parameters
                    SqlParameter parameter = new SqlParameter
                    {
                        ParameterName = "@Name",
                        Value = employee.Name,
                        SqlDbType = SqlDbType.VarChar,
                        Size = 255
                    };
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter
                    {
                        ParameterName = "@DepartmentID",
                        Value = employee.DepartmentID,
                        SqlDbType = SqlDbType.Int,
                    };
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter
                    {
                        ParameterName = "@DateOfBirth",
                        Value = employee.DateOfBirth,
                        SqlDbType = SqlDbType.Date
                    };
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter
                    {
                        ParameterName = "@EmployeeID",
                        Value = employee.EmployeeID, // Assuming 'EmployeeID' is the ID of the employee you want to update
                        SqlDbType = SqlDbType.Int
                    };
                    command.Parameters.Add(parameter);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return RedirectToAction("Index");
        }

        public IActionResult UpdateP(int id)
        {
            Products product = new Products();
            IEnumerable<Products> enumProduct = new ListService(Configuration).GetProductsList();

            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                var type = new ListService(Configuration).GetItemList();
                var department = new ListService(Configuration).GetDepartmentList();
                string sql = $"Select * From Product Where ProductId='{id}'";
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();

                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        product.ProductID = Convert.ToInt32(dataReader["ProductID"]);
                        product.Name = Convert.ToString(dataReader["Name"]);
                        product.Description = Convert.ToString(dataReader["Description"]);
                        product.Price = Convert.ToDecimal(dataReader["Price"]);
                        product.Url = Convert.ToString(dataReader["Url"]);
                        product.TypeID = Convert.ToInt32(dataReader["TypeID"]);
                        product.Type = type.Where(type => type.ItemId == product.TypeID).FirstOrDefault();
                        product.DepartmentID = Convert.ToInt32(dataReader["DepartmentID"]);
                        product.Department = department.Where(department => department.DepartmentId == product.DepartmentID).FirstOrDefault();
                        product.IsArchived = Convert.ToBoolean(dataReader["IsArchived"]);
                    }
                }
                connection.Close();
            }
            return View(product);
        }

        [HttpPost]
        public IActionResult UpdateP(Products product, int id)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
                TempData["SuccessMessage"] = "The product has been successfully created!";
            }

            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                string sql = $"UPDATE Product SET TypeID = '{product.TypeID}', Name = '{product.Name}', Description = '{product.Description}', DepartmentID = '{product.DepartmentID}', Price = '{product.Price}', URL = '{product.Url}' WHERE ProductID = '{id}'";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;

                    //adding parameters
                    SqlParameter parameter = new SqlParameter
                    {
                        ParameterName = "@TypeID",
                        Value = product.TypeID,
                        SqlDbType = SqlDbType.Int
                    };
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter
                    {
                        ParameterName = "@Name",
                        Value = product.Name,
                        SqlDbType = SqlDbType.VarChar,
                        Size = 50
                    };
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter
                    {
                        ParameterName = "@Description",
                        Value = product.Description,
                        SqlDbType = SqlDbType.VarChar,
                        Size = 255
                    };
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter
                    {
                        ParameterName = "@DepartmentID",
                        Value = product.DepartmentID,
                        SqlDbType = SqlDbType.Int
                    };
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter
                    {
                        ParameterName = "@Price",
                        Value = product.Price,
                        SqlDbType = SqlDbType.Decimal
                    };
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter
                    {
                        ParameterName = "@URL",
                        Value = product.Url,
                        SqlDbType = SqlDbType.VarChar,
                        Size = 255
                    };
                    command.Parameters.Add(parameter);


                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return RedirectToAction("Index", product);
        }


        [HttpPost]
        public IActionResult DeleteP(int id)
        {
           

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                

                string sql = $"UPDATE Product SET IsArchived = 1 WHERE ProductID = '{id}'";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;

                    // Add the @ProductId parameter
                    command.Parameters.Add(new SqlParameter("@ProductId", id));

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteE(int id)
        {


            using (SqlConnection connection = new SqlConnection(connectionString))
            {


                string sql = $"UPDATE Employee SET IsArchived = 1 WHERE EmployeeID = '{id}'";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;

                    // Add the @ProductId parameter
                    command.Parameters.Add(new SqlParameter("@EmployeeID", id));

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteD(int id)
        {


            using (SqlConnection connection = new SqlConnection(connectionString))
            {


                string sql = $"UPDATE Department SET IsArchived = 1 WHERE DepartmentID = '{id}'";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;

                    // Add the @ProductId parameter
                    command.Parameters.Add(new SqlParameter("@DepartmentID", id));

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return RedirectToAction("Index");
        }

        private List<Department> GetDepartmentList()
        {
            List<Department> departmentList = new List<Department>();

            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();

                string sql = "Select * From Department";
                SqlCommand cmd = new SqlCommand(sql, connection);

                using (SqlDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Department department = new Department();

                        department.DepartmentId = Convert.ToInt32(dataReader["DepartmentId"]);
                        department.DepartmentName = Convert.ToString(dataReader["DepartmentName"]);
                        department.DepartmentDesc = Convert.ToString(dataReader["DepartmentDesc"]);
                        department.IsArchived = Convert.ToBoolean(dataReader["IsArchived"]);

                        departmentList.Add(department);
                    }
                }
                connection.Close();
            }

            return departmentList;
        }

        private List<Products> GetProductList()
        {
            List<Products> productList = new List<Products>();
            IEnumerable<Products> enumProduct = new ListService(Configuration).GetProductsList();

            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                var type = new ListService(Configuration).GetItemList();
                var department = new ListService(Configuration).GetDepartmentList();

                connection.Open();

                //SQL Query command
                string sql = "Select * From Product";
                SqlCommand command = new SqlCommand(sql, connection);

                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Products product = new Products();

                        product.ProductID = Convert.ToInt32(dataReader["ProductID"]);
                        product.Name = Convert.ToString(dataReader["Name"]);
                        product.Description = Convert.ToString(dataReader["Description"]);
                        product.Price = Convert.ToDecimal(dataReader["Price"]);
                        product.Url = Convert.ToString(dataReader["Url"]);
                        product.TypeID = Convert.ToInt32(dataReader["TypeID"]);
                        product.Type = type.Where(type => type.ItemId == product.TypeID).FirstOrDefault();
                        product.DepartmentID = Convert.ToInt32(dataReader["DepartmentID"]);
                        product.Department = department.Where(department => department.DepartmentId == product.DepartmentID).FirstOrDefault();
                        product.IsArchived = Convert.ToBoolean(dataReader["IsArchived"]);

                        productList.Add(product);
                    }
                }
                connection.Close();
            }

            return productList;
        }

        private List<AboutUs> GetEmployeeList()
        {
            List<AboutUs> employeeList = new List<AboutUs>();

            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                var department = new ListService(Configuration).GetDepartmentList();

                connection.Open();

                //SQL Query command
                string sql = "Select * From Employee";
                SqlCommand command = new SqlCommand(sql, connection);

                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        AboutUs employee = new AboutUs();

                        employee.EmployeeID = Convert.ToInt32(dataReader["EmployeeID"]);
                        employee.DepartmentID = Convert.ToInt32(dataReader["DepartmentID"]);
                        employee.Department = department.Where(department => department.DepartmentId == employee.DepartmentID).FirstOrDefault();
                        employee.Name = Convert.ToString(dataReader["Name"]);
                        employee.DateOfBirth = DateTime.Parse(dataReader["DateOfBirth"].ToString());
                        employee.IsArchived = Convert.ToBoolean(dataReader["IsArchived"]);

                        employeeList.Add(employee);
                    }
                    
                }
                connection.Close();
            }
            return employeeList;
        }

        [HttpPost]
        public IActionResult DeleteSM(int id)
        {


            using (SqlConnection connection = new SqlConnection(connectionString))
            {


                string sql = $"UPDATE ServiceMessage SET IsArchived = 1 WHERE ServiceMessageID = '{id}'";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;

                    // Add the @ServiceMessageID parameter
                    command.Parameters.Add(new SqlParameter("@ServiceMessageID", id));

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return RedirectToAction("Index");
        }

        private List<ServiceMessage> GetServiceMessageList()
        {
            List<ServiceMessage> serviceMessages = new List<ServiceMessage>();

            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                string sql = "SELECT * FROM [dbo].[ServiceMessage] ORDER BY ServiceMessageID DESC";

                connection.Open();

                using (SqlDataReader reader = new SqlCommand(sql, connection).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        serviceMessages.Add(new ServiceMessage
                        {

                            ServiceMessageID = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Email = reader.GetString(2),
                            Subject = reader.GetString(3),
                            Message = reader.GetString(4),
                            CustomerID = reader.IsDBNull(5) ? null : (int?)reader.GetInt32(5),
                            EmployeeID = reader.IsDBNull(6) ? null : (int?)reader.GetInt32(6),
                            DateSent = reader.IsDBNull(7) ? (DateTime?)null : reader.GetDateTime(7),
                            IsArchived = reader.GetBoolean(8),
                        }) ;
                    }
                }

                connection.Close();
            }
        

            return serviceMessages; // Return service messages to view
        }

        private List<FeedBack> GetFeedBackList()
        {
            List<FeedBack> feedBackList = new List<FeedBack>();

            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                string sql = "SELECT * FROM [dbo].[FeedBack] ORDER BY FeedBackID DESC";

                connection.Open();

                using (SqlDataReader reader = new SqlCommand(sql, connection).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        feedBackList.Add(new FeedBack
                        {
                            FeedBackID = reader.GetInt32(0),
                            CustomerID = reader.IsDBNull(1) ? null : reader.GetInt32(1),
                            CustomerName = reader.GetString(2),
                            Rating = reader.GetInt32(3),
                            Comment = reader.GetString(4),
                            DateSubmitted = reader.GetDateTime(5),
                           
                        });
                    }
                }

                connection.Close();
            }


            return feedBackList; // Return service messages to view
        }

    }
}
