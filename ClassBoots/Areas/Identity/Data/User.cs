using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassBoots.Models;
using Microsoft.AspNetCore.Identity;

namespace ClassBoots.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the User class
    public class User : IdentityUser
    {
        [PersonalData]
        public string Name { get; set; }
        [PersonalData]
        public DateTime DOB { get; set; }
        [PersonalData]
        public string Role { get; set; }
        [PersonalData]
        public string History { get; set; }
    }
}
