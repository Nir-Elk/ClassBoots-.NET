using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassBoots.Models
{
    public class Institution
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public string GeoLocation { get; set; }
        public string Image{ get; set; }
    }
}