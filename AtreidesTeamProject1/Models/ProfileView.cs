using System.ComponentModel.DataAnnotations;

namespace AtreidesTeamProject1.Models
{
    public class ProfileView
    {
        [Required(ErrorMessage = "Name is required.")]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Name can only contain letters.")]
        public string Name { get; set; }

        public string UserName { get; set; }

        public string Address { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression(@"^\d{10,15}$", ErrorMessage = "Phone should be a number and contain between 10 to 15 digits.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; }

        public string Country { get; set; }
    }
}
