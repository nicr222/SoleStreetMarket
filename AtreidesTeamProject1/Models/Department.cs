using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AtreidesTeamProject1.Models
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }

        [BindProperty]
        [RegularExpression("^[a-zA-Z_ ]*$", ErrorMessage = "Name must contain only letters.")]
        [MaxLength(50, ErrorMessage = "You must enter a Name less than 50 characters")]
        [Required(ErrorMessage = "You must enter a name.")]
        public string DepartmentName { get; set;}

        [BindProperty]
        [RegularExpression("^[a-zA-Z_ ]*$", ErrorMessage = "Description must contain only letters.")]
        [MaxLength(50, ErrorMessage = "You must enter a Description less than 50 characters")]
        [Required(ErrorMessage = "You must enter a description.")]
        public string DepartmentDesc { get; set;}

        public bool IsArchived { get; set; }

        public string ToString()
        {
            return $"{DepartmentName}";
        }
    }
}
