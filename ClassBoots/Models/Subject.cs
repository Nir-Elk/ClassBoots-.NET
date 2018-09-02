using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClassBoots.Models
{
    public class Subject
    {
        [Key]
        public int ID { get; set; }
        public int SchoolID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
