using AtreidesTeamProject1.Models;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace AtreidesTeamProject1.Services
{
    public class ListService : IListService
    {
        private readonly string connectionString;

        public ListService(IConfiguration configuration)
        {
            Configuration = configuration;
            connectionString = Configuration["ConnectionStrings:DefaultConnection"];
        }

        public IConfiguration Configuration { get; }

        public IEnumerable<Department> GetDepartmentList()
        {
            List<Department> departmentList = new List<Department>();

            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                DataTable dataTable = new DataTable();

                string sql = "Select * From Department";
                SqlCommand cmd = new SqlCommand(sql, connection);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                adapter.Fill(dataTable);

                foreach (DataRow row in dataTable.Rows)
                {
                    departmentList.Add(
                        new Department
                        {
                            DepartmentId = Convert.ToInt32(row["DepartmentID"]),
                            DepartmentName = Convert.ToString(row["DepartmentName"]),
                            DepartmentDesc = Convert.ToString(row["DepartmentDesc"]),
                        });
                }
                return departmentList;
            }
            
        }

        public IEnumerable<Item> GetItemList()
        {
            List<Item> itemList = new List<Item>();

            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                DataTable dataTable = new DataTable();

                string sql = "Select * From Item";
                SqlCommand cmd = new SqlCommand(sql, connection);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                adapter.Fill(dataTable);

                foreach (DataRow row in dataTable.Rows)
                {
                    itemList.Add(
                        new Item
                        {
                            ItemId = Convert.ToInt32(row["ItemID"]),
                            Description = Convert.ToString(row["Description"]),
                            Name = Convert.ToString(row["Name"]),
                        });
                }
                return itemList;
            }
        }

        public IEnumerable<Products> GetProductsList()
        {
            List<Products> productsList = new List<Products>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                DataTable dataTable = new DataTable();

                string sql = "Select * From Product";
                SqlCommand command = new SqlCommand(sql, connection);

                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);

                //filling records to DataTable
                dataAdapter.Fill(dataTable);

                foreach (DataRow prd in dataTable.Rows)
                {
                    productsList.Add(
                        new Products
                        {
                            ProductID = Convert.ToInt32(prd["ProductID"]),
                            TypeID = Convert.ToInt32(prd["TypeID"]),
                            Name = Convert.ToString(prd["Name"]),
                            Description = Convert.ToString(prd["Description"]),
                            DepartmentID = Convert.ToInt32(prd["DepartmentID"]),
                            Price = Convert.ToInt32(prd["Price"]),
                            Url = Convert.ToString(prd["Url"])
                        });
                }
            }
            return productsList;
        }

        public IEnumerable<Customer> GetCustomerList()
        {
            List<Customer> customerList = new List<Customer>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                DataTable dataTable = new DataTable();

                string sql = "Select * From Customer";
                SqlCommand command = new SqlCommand(sql, connection);

                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);

                //filling records to DataTable
                dataAdapter.Fill(dataTable);

                foreach (DataRow prd in dataTable.Rows)
                {
                    customerList.Add(
                        new Customer
                        {
                            CustomerID = Convert.ToInt32(prd["CustomerID"]),
                            
                            Name = Convert.ToString(prd["Name"]),
                            Address = Convert.ToString(prd["Address"]),
                            PhoneNum = Convert.ToString(prd["Phone"]),
                            Email = Convert.ToString(prd["Email"]),
                            //Username = Convert.ToString(prd["UserName"]),
                            //Password = Convert.ToString(prd["Password"]),
                            //Salt = Convert.ToString(prd["Salt"])
                        });
                }
            }
            return customerList;
        }

        public IEnumerable<AboutUs> GetEmployeeList()
        {
            List<AboutUs> employeeList = new List<AboutUs>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                DataTable dataTable = new DataTable();

                string sql = "Select * From Employee";
                SqlCommand command = new SqlCommand(sql, connection);

                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);

                //filling records to DataTable
                dataAdapter.Fill(dataTable);

                foreach (DataRow prd in dataTable.Rows)
                {
                    employeeList.Add(
                        new AboutUs
                        {
                            EmployeeID = Convert.ToInt32(prd["EmployeeID"]),
                            DepartmentID = Convert.ToInt32(prd["DepartmentID"]),
                            Name = Convert.ToString(prd["Name"]),
                            DateOfBirth = Convert.ToDateTime(prd["DateOfBirth"])

                        });
                }
                
            }
            return employeeList;
        }

		public IEnumerable<UserRoles> GetRoleList()
		{
			List<UserRoles> roleList = new List<UserRoles>();

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				DataTable dataTable = new DataTable();

				string sql = "Select * From UserRole";
				SqlCommand command = new SqlCommand(sql, connection);

				SqlDataAdapter dataAdapter = new SqlDataAdapter(command);

				//filling records to DataTable
				dataAdapter.Fill(dataTable);

				foreach (DataRow prd in dataTable.Rows)
				{
					roleList.Add(
						new UserRoles
						{
							UserRoleID = Convert.ToInt32(prd["UserRoleID"]),
							
							Role = Convert.ToString(prd["Role"]),
							

						});
				}

			}
			return roleList;
		}
	}
}
