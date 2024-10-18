﻿using System.ComponentModel.DataAnnotations;

namespace Talabat.DTO_s
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
