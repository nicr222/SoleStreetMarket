using System.ComponentModel.DataAnnotations;

namespace AtreidesTeamProject1.Models
{
    public class FeedBack
    {
        [Key]
        public int FeedBackID { get; set; }

        public int? CustomerID { get; set; }

        public string CustomerName { get; set; }

        public int Rating { get; set; }

        public string Comment { get; set; }

        public DateTime DateSubmitted { get; set; }
    }
}
