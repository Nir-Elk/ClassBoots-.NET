using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClassBoots.Models
{
    public class Video
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public int LectureID { get; set; }
        public string URL { get; set; }
        public int Views { get; set; }
        public int Position { get; set; }
        public string OwnerID { get; set; }
    }
}
