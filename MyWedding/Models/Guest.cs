﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MyWedding.Models
{
    public class Guest
    {
        [Key]
        public string id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "First name required")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Surname required")]
        public string Surname { get; set; }

        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\+?([0-9]{10})([0-9]{0,3})?$", ErrorMessage = "Invalid number")]
        public string mobile { get; set; }

        [Required(ErrorMessage = "Email required")]
        [EmailAddress(ErrorMessage = "Please enter a valid email format")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{1,15}$", ErrorMessage = "Invalid email")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [MaxLength(200)]
        [DataType(DataType.MultilineText)]
        public string Message { get; set; }

        public bool IsAttending { get; set; }

        public DateTime dateAdded { get; set; }

        public DateTime dateUpdated { get; set; }

        public bool Approved { get; set; }
    }
}
