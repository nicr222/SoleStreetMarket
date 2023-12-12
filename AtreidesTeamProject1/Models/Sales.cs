using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace AtreidesTeamProject1.Models
{
    public class Sales
    {
        [Key]
        public int SalesId { get; set; }

        public int CustomerID { get; set; }

        [ValidateNever]
        public Customer Customer { get; set; }

        [ValidateNever]
        public DateTime OrderDate { get; set; }

        public int Quantity { get; set; }

        public decimal Total { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Name should contain only letters.")]
        public string Name { get; set; }

        [BindProperty]
        [RegularExpression("^[0-9].{15}$", ErrorMessage = "Card Number should contain exactly 16 digits that are only numbers.")]
        [Required(ErrorMessage = "Card Number is required.")]
        public double CardNumber { get; set; }

        [BindProperty]
        [RegularExpression("^(1[0-2]|0[1-9]|[1-9])\\/(20[2-9]\\d|2\\d{3}|19\\d{2}|0(?!0)\\d|[1-9]\\d)$", ErrorMessage = "You need to enter a future date format as (mm/yyyy) or (mm/yy)")]
        [Required(ErrorMessage = "Expiry is required.")]
        public string Expiry { get; set; }

        [BindProperty]
        [RegularExpression("^[0-9].{2}$", ErrorMessage = "CVC should contain exactly 3 digits that are only numbers.")]
        [Required(ErrorMessage = "CVC is required.")]
        public int CVC { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Street Address is required.")]
        public string StreetAddress { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "City is required.")]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "City should contain only letters.")]
        public string City { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "State is required.")]
        public string State { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Zipcode is required.")]
        [RegularExpression("^[1-9].{4}$", ErrorMessage = "Zipcode should be a 5-digit number that is a valid zipcode.")]
        public int Zipcode { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Email is not valid.")]
        public string Email { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Phone is required.")]
        [RegularExpression("^[1-9].{9}$", ErrorMessage = "Your phone number should have numbers only and must be a valid number")]
        [Phone(ErrorMessage = "Phone number is not valid.")]
        public string Phone { get; set; }
    }
}
