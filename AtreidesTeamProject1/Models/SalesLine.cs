using System.ComponentModel.DataAnnotations;

namespace AtreidesTeamProject1.Models
{
    public class SalesLine
    {
        [Key]
        public int SalesLineId { get; set; }

        public int ProductID { get; set; }

        public Products ProductName { get; set; }

        public int SalesID { get; set; }

        public Sales Sale { get; set; }

        public int Quantity { get; set; }

        public double Total { get; set; }

        public int MonthlySales { get; set; }

        public int TotalSales { get; set; }

        public decimal TotalEarnings { get; set; }

        public decimal Profits { get; set; }
    }
}