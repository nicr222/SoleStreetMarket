using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace AtreidesTeamProject1.Models
{
    public class Products
    {
        [Key]
        public int ProductID { get; set; }

        [ValidateNever]
        public Item Type { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "You must enter a description.")]
        public int TypeID { get; set; }

        [BindProperty]
        [RegularExpression("^[a-zA-Z_ ]*$", ErrorMessage = "Name must contain only letters.")]
        [MaxLength(50, ErrorMessage = "You must enter a Name less than 50 characters")]
        [Required(ErrorMessage = "You must enter a name.")]
        public string Name { get; set; }

        [BindProperty]
        [RegularExpression("^[a-zA-Z_ ]*$", ErrorMessage = "Description must contain only letters.")]
        [MaxLength(50, ErrorMessage = "You must enter a Description less than 50 characters")]
        [Required(ErrorMessage = "You must enter a description.")]
        public string Description { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "You must select a value.")]
        public int DepartmentID { get; set; }

        [ValidateNever]
        public Department Department { get; set; }

        [BindProperty]
        [Range(1, 600, ErrorMessage = "Price must be between $1.00 and $600.00.")]
        [DisplayFormat(DataFormatString = "{0:#,###.00}")]
        [Required(ErrorMessage = "You must enter a price.")]
        public decimal Price { get; set; }

        [BindProperty]
        [RegularExpression("^((https?\\:\\/\\/)|(\\/{1,2})).*?$", ErrorMessage = "Url must contain a valid url.")]
        [Required(ErrorMessage = "You must enter a image url.")]
        public string Url { get; set; }

        public ItemCategory Category { get; set; }

        public bool IsArchived { get; set; }
    }
}
