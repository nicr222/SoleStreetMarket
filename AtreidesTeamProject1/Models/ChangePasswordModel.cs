using System.ComponentModel.DataAnnotations;

namespace AtreidesTeamProject1.Models
{
    public class ChangePasswordModel
    {
            [Required(ErrorMessage = "Current Password is required.")]
            public string CurrentPassword { get; set; }

            [Required(ErrorMessage = "New Password is required.")]
            [MinLength(8, ErrorMessage = "Password should be at least 8 characters long.")]
            public string NewPassword { get; set; }

            [Required(ErrorMessage = "Confirm New Password is required.")]
            [Compare("NewPassword", ErrorMessage = "New Password and Confirmation Password do not match.")]
            public string ConfirmNewPassword { get; set; }

    }
}
