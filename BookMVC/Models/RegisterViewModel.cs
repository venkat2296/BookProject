using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookMVC.Models
{
    public class RegisterViewModel
    {
        [Required]
        public string UserName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Confirm Your Password")]
        [Compare("Password", ErrorMessage = "Passwords are not matching")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
