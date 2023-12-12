using AtreidesTeamProject1.Models;
using AtreidesTeamProject1.Services;
using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using NuGet.Protocol.Plugins;
using System.Data.SqlClient;

namespace AtreidesTeamProject1.Controllers
{
    public class CartController : Controller
    {
        //Setting the default connection string
        private string connectionString;
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Initialized a new instance of the <see cref="ProductController"/> class.
        /// </summary>
        /// <param name="configuration"></param>
        public CartController(IConfiguration configuration)
        {
            Configuration = configuration;
            connectionString = Configuration["ConnectionStrings:DefaultConnection"];
        }

        public IActionResult Index()
        {
            //Creating variables.
            List<Cart> cartList = new List<Cart>();
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                //Gettting a list of all the products in the products table
                var products = new ListService(Configuration).GetProductsList();

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
                        cart.Products = products.Where(products => products.ProductID == cart.ProductID).FirstOrDefault();
                        cart.DepartmentID = Convert.ToInt32(dataReader["DepartmentID"]);
                        cart.Quantity = Convert.ToInt32(dataReader["Quantity"]);
                        ///Think that I need to do a GetDepartmentList to get the department list.

                        cartList.Add(cart);
                    }
                }
                connection.Close();
            }
            return View(cartList);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            //Creating variables.
            List<Cart> cartList = new List<Cart>();

            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                var products = new ListService(Configuration).GetProductsList();

                //opening the connection
                connection.Open();

                ///This gets the product from the cart where it is the same id as the one selected.
                string sql = $"Select * From Cart WHERE ProductID='{id}'";
                SqlCommand command = new SqlCommand(sql, connection);

                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Cart cart = new Cart();

                        cart.ProductID = Convert.ToInt32(dataReader["ProductID"]);
                        cart.Products = products.Where(products => products.ProductID == cart.ProductID).FirstOrDefault();
                        cart.DepartmentID = Convert.ToInt32(dataReader["DepartmentID"]);
                        cart.Quantity = Convert.ToInt32(dataReader["Quantity"]);
                        ///Think that I need to do a GetDepartmentList to get the department list.

                        cartList.Add(cart);
                    }
                }
                connection.Close();

                //If the quantity of the item in the cart will be 0 after we
                //delete one from the quantity it will go and delete the product from the cart
                if ((cartList[0].Quantity - 1) == 0)
                {
                    //delete from the table
                    sql = $"Delete From [Cart] Where ProductID='{id}'";

                    using (command = new SqlCommand(sql, connection))
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
                //If the quantity will be greater than 0 after one is taken away it will run
                else if ((cartList[0].Quantity - 1) > 0)
                {
                    //Delete one quantity from the table
                    cartList[0].Quantity--;

                    //update the quantity to the existing product.
                    sql = $"Update Cart SET Quantity='{cartList[0].Quantity}'  Where ProductID='{id}'";

                    //add a using sql command
                    using (command = new SqlCommand(sql, connection))
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
            }
            return RedirectToAction("Index");
        }

        /*[HttpPost]
        public IActionResult AddToSales(Sales sales)
        {

        }*/


        /*  [HttpPost]
          public IActionResult AddToCart(int productId)
          {
              List<Cart> cart = new List<Cart>();
              var existingItem = cart.Find(item => item.ProductID == productId);
              foreach (Products p in Repository.AllProducts)
              {
                  if (productId == p.ProductID)
                  {
                      if (existingItem != null)
                      {
                          existingItem.Quantity++;
                      }
                      else
                      {
                          cart.Add(new Cart
                          {
                              ProductID = p.ProductID,
                              Description = p.Description,
                              Price = p.Price,
                              Quantity = 1
                          });
                      }
                  }
              }

             // HttpContext.Session.Set("Cart", cart);
              return RedirectToAction("Index");
          }*/

        //This will display the cart checkout form
        public IActionResult CartInfoForm(decimal total, int quantity)
        {
            //If the total is not null it will enter the checkout page otherwise it will display a message stating that you need products in your cart
            if (total != 0)
            {
                return RedirectToAction("AddToSales", "Sales", new { total, quantity });
            }
            else
            {
                TempData["ErrorMessage"] = "You must have products in your cart to checkout!";
                return RedirectToAction("Index");
            }
        }
    }
}
