namespace AtreidesTeamProject1.Models
{
    public class AllModels
    {
        public IEnumerable<AboutUs> Employees { get; set; }

        public IEnumerable<Cart> Cart { get; set; }

        public IEnumerable<Customer> Customer { get; set; }

        public IEnumerable<Department> Department { get; set; }

        public IEnumerable<FeedBack> FeedBack { get; set; }

        public IEnumerable<Item> Item { get; set; }

        public IEnumerable<Products> Products { get; set; }

        public IEnumerable<ServiceMessage> ServiceMessages { get; set; }

        public IEnumerable<Sales> Sales { get; set; }
    }
}
