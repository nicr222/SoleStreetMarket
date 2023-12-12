using AtreidesTeamProject1.Models;
using AtreidesTeamProject1.Services;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AtreidesTeamProject1.Controllers
{
    public class ProductController : Controller
    {
        //Setting the default connection string
        private string connectionString;
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Initialized a new instance of the <see cref="ProductController"/> class.
        /// </summary>
        /// <param name="configuration"></param>
        public ProductController(IConfiguration configuration)
        {
            Configuration = configuration;
            connectionString = Configuration["ConnectionStrings:DefaultConnection"];
        }

        // Index action method with an optional parameter 'category' which defaults to 'All' if not provide
        public IActionResult Index(ItemCategory category = ItemCategory.All)
        {
            List<Products> productList = new List<Products>();
            IEnumerable<Products> enumProduct = new ListService(Configuration).GetProductsList();

            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                // Retrieve item and department types from a service
                var type = new ListService(Configuration).GetItemList();
                var department = new ListService(Configuration).GetDepartmentList();

                connection.Open();

                //SQL Query command
                string sql = "Select * From Product";
                SqlCommand command = new SqlCommand(sql, connection);

                // Execute the SQL query and process the results
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    // Loop through each row in the result set
                    while (dataReader.Read())
                    {
                        Products product = new Products();

                        // Assign properties to the product object from the database row
                        product.ProductID = Convert.ToInt32(dataReader["ProductID"]);
                        product.Name = Convert.ToString(dataReader["Name"]);
                        product.Description = Convert.ToString(dataReader["Description"]);
                        product.Price = Convert.ToDecimal(dataReader["Price"]);
                        product.Url = Convert.ToString(dataReader["Url"]);
                        product.TypeID = Convert.ToInt32(dataReader["TypeID"]);
                        product.Type = type.Where(type => type.ItemId == product.TypeID).FirstOrDefault();
                        product.DepartmentID = Convert.ToInt32(dataReader["DepartmentID"]);
                        product.Department = department.Where(department => department.DepartmentId == product.DepartmentID).FirstOrDefault();

                        productList.Add(product);
                    }
                }
                connection.Close();
            }
            // Filter the products by the category if it is not 'All'
            if (category != ItemCategory.All)
            {
                enumProduct = productList.Where(p => p.Type.Name == category.ToString());
                // Return the filtered list of products to the view
                return View(enumProduct.ToList());
            }
            else
            {
                return View(productList);
            }
        }

        /// <summary>
        /// Adding the product to the cart when it is clicked in the product page.
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public IActionResult AddToCart(int productID, int departmentID)
        {
            //Creating variables.
            List<Cart> cartList = new List<Cart>();
            var counter = 1;

            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                //opening the connection
                connection.Open();

                ///This gets the data that is alread in the cart.
                //Query command
                string sql = "Select * From Cart";
                SqlCommand command = new SqlCommand(sql, connection);

                using (SqlDataReader dataReader = command.ExecuteReader()) 
                { 
                    while (dataReader.Read())
                    {
                        Cart cart = new Cart();

                        cart.ProductID = Convert.ToInt32(dataReader["ProductID"]);
                        cart.DepartmentID = Convert.ToInt32(dataReader["DepartmentID"]);
                        cart.Quantity = Convert.ToInt32(dataReader["Quantity"]);

                        cartList.Add(cart);
                    }
                }
                connection.Close();



                //This will see if anything is in the cart and if nothing is in the cart it will add it to the cart
                if (cartList.Count == 0)
                {
                    //This will take the info from the page and input into the cart table.
                    sql = "Insert Into Cart (ProductID, DepartmentID, Quantity) Values (@ProductID, @DepartmentID, @Quantity)";
                    //add a using sql command 

                    using (command = new SqlCommand(sql, connection))
                    {
                        command.CommandType = CommandType.Text;

                        //add the product to the database.
                        SqlParameter parameter = new SqlParameter
                        {
                            ParameterName = "@ProductID",
                            Value = productID,
                            SqlDbType = SqlDbType.Int
                        };
                        command.Parameters.Add(parameter);

                        parameter = new SqlParameter
                        {
                            ParameterName = "@DepartmentID",
                            Value = departmentID,
                            SqlDbType = SqlDbType.Int
                        };
                        command.Parameters.Add(parameter);

                        parameter = new SqlParameter
                        {
                            ParameterName = "@Quantity",
                            Value = 1,
                            SqlDbType = SqlDbType.Int
                        };
                        command.Parameters.Add(parameter);

                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }

                    return RedirectToAction("Index");
                }
                //This runs if there is something in the cart
                else
                {
                    //This runs through everything in the cart table
                    foreach (Cart cart in cartList)
                    {
                        //This will test to see if the product is in the table already and will update the quantity in the table.
                        if (cart.ProductID == productID)
                        {
                            //Updating the cart quantity for that product
                            cart.Quantity++;

                            //add quantity to the existing product.
                            sql = $"Update Cart SET Quantity='{cart.Quantity}'  Where ProductID='{productID}'";

                            //add a using sql command
                            using (command = new SqlCommand(sql, connection))
                            {
                                connection.Open();
                                command.ExecuteNonQuery();
                                connection.Close();
                            }
                            return RedirectToAction("Index");
                        }
                        //This will run if the cart has items in the cart but not the item that you are tring to add
                        else if (counter == cartList.Count)
                        {
                            sql = "Insert Into Cart (ProductID, DepartmentID, Quantity) Values (@ProductID, @DepartmentID, @Quantity)";
                            //add a using sql command 

                            using (command = new SqlCommand(sql, connection))
                            {
                                command.CommandType = CommandType.Text;

                                //add the product to the database.
                                SqlParameter parameter = new SqlParameter
                                {
                                    ParameterName = "@ProductID",
                                    Value = productID,
                                    SqlDbType = SqlDbType.Int
                                };
                                command.Parameters.Add(parameter);

                                parameter = new SqlParameter
                                {
                                    ParameterName = "@DepartmentID",
                                    Value = departmentID,
                                    SqlDbType = SqlDbType.Int
                                };
                                command.Parameters.Add(parameter);

                                parameter = new SqlParameter
                                {
                                    ParameterName = "@Quantity",
                                    Value = 1,
                                    SqlDbType = SqlDbType.Int
                                };
                                command.Parameters.Add(parameter);

                                connection.Open();
                                command.ExecuteNonQuery();
                                connection.Close();
                            }
                        }
                        counter++;
                    }
                }
            }

            return RedirectToAction("Index");
        }
    }
}
