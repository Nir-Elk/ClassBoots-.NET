using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassBoots.Models
{
    public class Path
    {
        public Institution institution { get; set; }
        public School school { get; set; }
        public Subject subject { get; set; }
        public Lecture lecture { get; set; }
        public Video video { get; set; }

        public Path(Institution institution, School school, Subject subject, Lecture lecture, Video video)
        {
            this.institution = institution;
            this.school = school;
            this.subject = subject;
            this.lecture = lecture;
            this.video = video;
        }

        public Path() { }
    }

}
