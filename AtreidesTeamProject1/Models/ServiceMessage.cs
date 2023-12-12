using System.ComponentModel.DataAnnotations;

namespace AtreidesTeamProject1.Models
{
    public class ServiceMessage
    {
        [Key]
        public int ServiceMessageID { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Subject { get; set; }

        public string Message { get; set; }

        public int? CustomerID { get; set; } //Nullable

        public Customer Customer { get; set; }

        public int? EmployeeID { get; set; } //Nullable

        public AboutUs Employee { get; set; }

        public DateTime? DateSent { get; set; } // Make it nullable there are old entries without a timestamp.

        public bool IsArchived { get; set; } 
    }
}
