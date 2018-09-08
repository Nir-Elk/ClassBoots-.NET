using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ClassBoots.Models;

namespace ClassBoots.Models
{
    public class ModelContext : DbContext
    {
        public ModelContext (DbContextOptions<ModelContext> options)
            : base(options)
        {
        }

        public DbSet<ClassBoots.Models.School> School { get; set; }

        public DbSet<ClassBoots.Models.Institution> Institution { get; set; }

        public DbSet<ClassBoots.Models.Lecture> Lecture { get; set; }

        public DbSet<ClassBoots.Models.Subject> Subject { get; set; }

        public DbSet<ClassBoots.Models.Video> Video { get; set; }
    }
}
