﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MyWedding.Models
{
    public class LoginUser
    {

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email")]
        public string Username { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool rememberme { get; set; } = false;


        public string ReturnUrl { get; set; }
    }
}
