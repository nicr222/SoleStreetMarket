using System.ComponentModel.DataAnnotations;

namespace AtreidesTeamProject1.Models
{
    public class Customer
    {
        [Key]
        public int CustomerID { get; set; }

        public int UserID { get; set; }

        public string Name { get; set; }

        public string? Address { get; set; }

        public string? Country { get; set; }

        public string PhoneNum { get; set; }

        public string Email { get; set; }

        public string ToString()
        {
            return Name;
        }
    }
}

