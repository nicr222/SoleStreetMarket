using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace AtreidesTeamProject1.Models
{
    public class Cart
    {
        [Key]
        public int CartID { get; set; }

        public int ProductID { get; set; }

        public Products Products { get; set; }

        public string Description { get; set; }

        public int DepartmentID { get; set; }
        public Department Department { get; set; }

        public double Price { get; set; }

        public double Total { get; set; }

        public int Quantity { get; set; }
    }
}
