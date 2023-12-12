using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace AtreidesTeamProject1.Models
{
	public class OESContext : IdentityDbContext
	{
		public OESContext(DbContextOptions<OESContext> options) : base(options)
		{
		}

		public DbSet<AboutUs> Employees { get; set; }

		public DbSet<Cart> Cart { get; set; }

		public DbSet<User> Users { get; set; }

		public DbSet<employeeSales> EmployeesSales { get; set; }

		public DbSet<SalesLine> SalesLines { get; set; }

		public DbSet<Customer> Customer { get; set; }

		public DbSet<Department> Department { get; set; }

		public DbSet<FeedBack> FeedBack { get; set; }

		public DbSet<Item> Item { get; set; }

		public DbSet<Products> Products { get; set; }

		public DbSet<ServiceMessage> ServiceMessages { get; set; }

		public DbSet<Sales> Sales { get; set; }

		

		
	}
}
