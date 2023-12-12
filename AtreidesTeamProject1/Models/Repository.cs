namespace AtreidesTeamProject1.Models
{
    public static class Repository
    {
        private static List<AboutUs> allEmployees;
        private static List<Customer> allCustomer;
        private static List<Department> allDepartment;
        private static List<employeeSales> allEmployeesSales;
        private static List<Item> allItems;
        private static List<Products> allProducts;
        private static List<Sales> allSales;
        private static List<SalesLine> allSalesLine;

        public static IEnumerable<AboutUs> AllEmployees
        {
            get
            {
                if(allEmployees == null)
                {
                    allEmployees = new List<AboutUs>();

                    //allEmployees.Add(new AboutUs { EmployeeID = 1, Name = "Phoo Myint", DateOfBirth = "11/23/1996"});
                    //allEmployees.Add(new AboutUs { EmployeeID = 2, Name = "Josh Halat", DateOfBirth = "02/03/2004" });
                    //allEmployees.Add(new AboutUs { EmployeeID = 3, Name = "Nicolas Ross", DateOfBirth = "05/30/1992" });
                    //allEmployees.Add(new AboutUs { EmployeeID = 4, Name = "Lucas Radke", DateOfBirth = "03/22/2005" });
                }
                return allEmployees;
            }
        }

        public static IEnumerable<Customer> AllCustomer
        {
            get
            {
                if (allCustomer == null)
                {
                   
                    allCustomer = new List<Customer>();

                    allCustomer.Add(new Customer { CustomerID = 1, Name = "Walter Harris", Address = "789 Center Rd", PhoneNum = "123-456-7890", Email = "walter@example.com" });
                    allCustomer.Add(new Customer { CustomerID = 2, Name = "John Smith", Address = "123 Main St", PhoneNum = "555-555-5555", Email = "john@example.com" });
                    allCustomer.Add(new Customer { CustomerID = 3, Name = "Mary Johnson", Address = "456 Elm St", PhoneNum = "777-888-9999", Email = "mary@example.com" });

                }
                return allCustomer;
                
            }
        }

        public static IEnumerable<Department> AllDepartment
        {
            get
            {
                if(allDepartment == null)
                {
                    allDepartment = new List<Department>();

                    //allDepartment.Add(new Department { DepartmentId = 2, DepartmentName = "Men's Shoes", DepartmentDescription = "Men's Shoe Department" });
                    // allDepartment.Add(new Department { DepartmentId = 3, DepartmentName = "Women's Shoes", DepartmentDescription = "Women's Shoe Department" });
                    //allDepartment.Add(new Department { DepartmentId = 4, DepartmentName = "Kids' Shoes", DepartmentDescription = "Kids' Shoe Department" });
                    //allDepartment.Add(new Department { DepartmentId = 5, DepartmentName = "Accessories", DepartmentDescription = "Shoe Accessories Department" });
                }
                return allDepartment;
            }
        }

        public static IEnumerable<employeeSales> AllEmployeeSales
        {
            get
            {
                if(allEmployeesSales == null)
                {
                    allEmployeesSales = new List<employeeSales>();

                    allEmployeesSales.Add(new employeeSales { EmployeeSalesID = 1});
                    allEmployeesSales.Add(new employeeSales { EmployeeSalesID = 2 });
                    allEmployeesSales.Add(new employeeSales { EmployeeSalesID = 3 });
                    allEmployeesSales.Add(new employeeSales { EmployeeSalesID = 4 });
                    allEmployeesSales.Add(new employeeSales { EmployeeSalesID = 5 });
                    allEmployeesSales.Add(new employeeSales { EmployeeSalesID = 6 });
                }
                return allEmployeesSales;
            }
        }

        public static IEnumerable<Item> AllItems
        {
            get
            {
                if (allItems == null)
                {
                    allItems = new List<Item>();

                   /* allItems.Add(new Item { ItemId = 1, Name = "Running Shoes", Description = "Lightweight running shoes for athletes" });
                    allItems.Add(new Item { ItemId = 2, Name = "Casual Sneakers", Description = "Comfortable sneakers for everyday wear" });
                    allItems.Add(new Item { ItemId = 3, Name = "High Heels", Description = "Elegant high-heeled shoes for formal occasions" });
                    allItems.Add(new Item { ItemId = 4, Name = "Hiking Boots", Description = "Sturdy hiking boots for outdoor adventures" });
                    allItems.Add(new Item { ItemId = 5, Name = "Kids' Sandals", Description = "Colorful and fun sandals for kids" });
                    allItems.Add(new Item { ItemId = 6, Name = "Slip-on Shoes", Description = "Convenient slip-on shoes for easy wearing" });
                    allItems.Add(new Item { ItemId = 7, Name = "Athletic Cleats", Description = "Cleated shoes for sports and athletics" });
                    allItems.Add(new Item { ItemId = 8, Name = "Orthopedic Shoes", Description = "Supportive orthopedic shoes for comfort" });*/
                }
                return allItems;
            }
               
        }

        public static IEnumerable<Products> AllProducts
        {
            get
            {
                if(allProducts == null)
                {
                    allProducts = new List<Products>();

                    allProducts.Add(new Products { ProductID = 1, Description = "Running Shoes", Price = 79.99m, Url = "https://th.bing.com/th/id/OIP.2yZTmrX2FAYYlL1B8YzhrgAAAA?pid=ImgDet&rs=1", Category = ItemCategory.Shoes });
                    allProducts.Add(new Products { ProductID = 2, Description = "Casual Sneakers", Price = 49.99m, Url = "https://th.bing.com/th/id/OIP.5k7byqYmnEU1-2uFEyknfgHaHa?pid=ImgDet&rs=1", Category = ItemCategory.Shoes });
                    allProducts.Add(new Products { ProductID = 3, Description = "High Heels", Price = 89.99m, Url = "https://th.bing.com/th/id/R.41535655c4dedd3670353471044e0592?rik=vwc0Ht9J2ociRg&riu=http%3a%2f%2fs7d1.scene7.com%2fis%2fimage%2fMoosejawMB%2f10250920x1093215_zm%3f%24product1000%24&ehk=lZb2FzgN%2bYSyUVzCsIuGrjgL1w8tqrBFzIHkh2EsEPA%3d&risl=&pid=ImgRaw&r=0", Category = ItemCategory.Heels });
                    allProducts.Add(new Products { ProductID = 4, Description = "Hiking Boots", Price = 69.99m, Url = "https://s7d1.scene7.com/is/image/MoosejawMB/10263990x1099959_zm?$product1000$", Category = ItemCategory.Boots });
                    allProducts.Add(new Products { ProductID = 5, Description = "Kids' Sandals", Price = 29.99m, Url = "https://th.bing.com/th/id/OIP.tcxY66n7Q7ZLvBfV70-rYQHaHa?pid=ImgDet&rs=1", Category = ItemCategory.Shoes });
                    allProducts.Add(new Products { ProductID = 6, Description = "Slip-on Shoes", Price = 39.99m, Url = "https://www.charlesclinkard.co.uk/images/products/1417608976-31492000.jpg", Category = ItemCategory.Shoes });
                    allProducts.Add(new Products { ProductID = 7, Description = "Athletic Cleats", Price = 59.99m, Url = "https://th.bing.com/th/id/R.b70ab74dc76adad72b1aab3f07612bfb?rik=DfCIzKNU0jh7yg&pid=ImgRaw&r=0", Category = ItemCategory.Shoes });
                    allProducts.Add(new Products { ProductID = 8, Description = "Work Boots", Price = 99.99m, Url = "https://i.pinimg.com/originals/85/a0/c7/85a0c737ff4148a8263a87770e37e106.jpg", Category = ItemCategory.Boots });

                }
                return allProducts;
            }
        }

        public static IEnumerable<Sales> AllSales
        {
            get
            {
                if (allSales == null)
                {
                    allSales = new List<Sales>();

                    allSales.Add(new Sales { SalesId = 1, Quantity = 3, Total = 149.97m });
                    allSales.Add(new Sales { SalesId = 2, Quantity = 2, Total = 99.98m });
                    allSales.Add(new Sales { SalesId = 3, Quantity = 1, Total = 89.99m });
                    allSales.Add(new Sales { SalesId = 4, Quantity = 5, Total = 349.95m });
                    allSales.Add(new Sales { SalesId = 5, Quantity = 4, Total = 119.96m });
                    allSales.Add(new Sales { SalesId = 6, Quantity = 2, Total = 79.98m });
                    allSales.Add(new Sales { SalesId = 7, Quantity = 1, Total = 59.99m });
                    allSales.Add(new Sales { SalesId = 8, Quantity = 3, Total = 209.97m });
                    allSales.Add(new Sales { SalesId = 9, Quantity = 2, Total = 69.98m });
                    allSales.Add(new Sales { SalesId = 10, Quantity = 4, Total = 199.96m });

                }
                return allSales;
            }
        }

        public static IEnumerable<SalesLine> AllSalesLine
        {
            get
            {
                if(allSalesLine == null)
                {
                    allSalesLine = new List<SalesLine>();

                    allSalesLine.Add(new SalesLine { SalesLineId = 1, Quantity = 2, Total = 99.98 });
                    allSalesLine.Add(new SalesLine { SalesLineId = 2, Quantity = 3, Total = 149.97 });
                    allSalesLine.Add(new SalesLine { SalesLineId = 3, Quantity = 1, Total = 49.99 });
                    allSalesLine.Add(new SalesLine { SalesLineId = 4, Quantity = 4, Total = 199.96 });
                    allSalesLine.Add(new SalesLine { SalesLineId = 5, Quantity = 2, Total = 79.98 });
                    allSalesLine.Add(new SalesLine { SalesLineId = 6, Quantity = 3, Total = 119.97 });
                    allSalesLine.Add(new SalesLine { SalesLineId = 7, Quantity = 5, Total = 249.95 });

                }
                return allSalesLine;
            }
        }
    }
}
