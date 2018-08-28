using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClassBoots.Models
{
    public class School
    {
        [Key]
        public int ID { get; set; }
        public int InstitutionID { get; set; }
        public string Name { get; set; }
    }
}
