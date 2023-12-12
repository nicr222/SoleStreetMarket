using System.ComponentModel.DataAnnotations;

namespace AtreidesTeamProject1.Models
{
    public class employeeSales
    {
        [Key]
        public int EmployeeSalesID { get; set; }

        public int EmployeeID { get; }

        public AboutUs Employee { get; set; }

        public int SalesID { get; }

        public Sales Sales { get; set; }
    }
}
