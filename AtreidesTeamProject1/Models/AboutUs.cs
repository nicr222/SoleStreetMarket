using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace AtreidesTeamProject1.Models
{
    public class AboutUs 
    {
        [Key]
        public int EmployeeID { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "You must select a value.")]
        public int DepartmentID { get; set; }

        [ValidateNever]
        public Department Department { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "You must enter a name.")]
        [RegularExpression("^[a-zA-Z_ ]*$", ErrorMessage = "Name must contain only letters.")]
        [MaxLength(50, ErrorMessage = "You must enter a Name less than 50 characters")]
        public string Name { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "You must enter a date.")]
        public DateTime DateOfBirth { get; set; }

        public int UserRoleID { get; set; }

        public UserRoles Role { get; set; }

        public bool IsArchived { get; set; }
    }
}
