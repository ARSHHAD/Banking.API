using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Domain.Models.ViewModels
{
    public class LoginModel
    {
        [Required]
        [StringLength(50, ErrorMessage = "Username must be at most 50 characters long.")]
        public string Username { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Password must be at least 8 characters long.")]
        public string Password { get; set; }
    }

}
