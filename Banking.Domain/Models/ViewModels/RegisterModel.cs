using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Domain.Models.ViewModels
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "User name is required")]
        [StringLength(50, ErrorMessage = "Username must be at most 50 characters long.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email should be valid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Date of Birth is required")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format.")]
        public DateTime? DateOfBirth { get; set; } = null;

        [Required(ErrorMessage = "Password is required")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 20 characters.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Password must include an uppercase letter, a lowercase letter, a number, and a special character.")]
        public string Password { get; set; }

        [Required(ErrorMessage ="Confirm password is required")]
        [StringLength(20, MinimumLength = 8)]
        public string ConfirmPassword { get; set; }
    }
}
