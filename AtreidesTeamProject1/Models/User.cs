using System.ComponentModel.DataAnnotations;

namespace AtreidesTeamProject1.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Salt { get; set; }
    }
}
