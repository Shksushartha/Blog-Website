using System;
using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModels
{
	public class RegisterVM
	{

        [Required]
        public string Username { get; set; } = null!;

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;

        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; } = null!;

        [DataType(DataType.Password)]
        [Compare("Password")]
        [Required]
        public string confirmPassword { get; set; } = null!;
    }
}

