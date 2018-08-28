using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClassBoots.Models
{
    public class Lecture
    {
        [Key]
        public int ID { get; set; }
        public int LecturerID { get; set; }
        public int SubjectID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Date { get; set; }
    }
}
