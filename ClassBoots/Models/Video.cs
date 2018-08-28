using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassBoots.Models
{
    public class Video
    {
        public int ID { get; set; }
        public int LectureID { get; set; }
        public string Refference { get; set; }
        public int Views { get; set; }
        public int Position { get; set; }
    }
}
