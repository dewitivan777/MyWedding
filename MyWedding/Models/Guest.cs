using System;
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
        [Required]
        public string name { get; set; }
        [Required]
        public string surname { get; set; }
        [EmailAddress(ErrorMessage = "Invalid format")]
        public string email { get; set; }

        [Phone(ErrorMessage = "Invalid format")]
        public string mobile { get; set; }
        public bool isattending { get; set; }
    }
}
