using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClassBoots.Models
{
    public class AppUser
    {
        [Key]
        public string Email { get; set; }
        [HiddenInput]
        public int GroupID { get; set; } = 0;
        [Required]
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last name")]
        public string LastName { get; set; }
    }
}
