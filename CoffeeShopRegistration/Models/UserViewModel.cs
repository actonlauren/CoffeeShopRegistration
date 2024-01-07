using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using System.ComponentModel.DataAnnotations;

namespace CoffeeShopRegistration.Models
{
    public class UserViewModel
    {
        [Display (Name = "Favorite Number")]
        public int FavoriteNumber { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "Try again")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Try again")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Try again")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Please try again")]
        public string Email { get; set; }
       
        [Required(ErrorMessage = "Try again")]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Compare("Password" ,ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }


        [Required(ErrorMessage = "Try again")]
        public int Age { get; set; }

       
        [Required(ErrorMessage = "Try again")]
        public string City { get; set; }

        [Required(ErrorMessage = "Try again")]
        public string State { get; set; }


    }
}
