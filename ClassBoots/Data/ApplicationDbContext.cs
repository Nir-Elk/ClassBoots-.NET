using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ClassBoots.Models;

namespace ClassBoots.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ClassBoots.Models.Subject> Subject { get; set; }
        public DbSet<ClassBoots.Models.Video> Video { get; set; }
        public DbSet<ClassBoots.Models.School> School { get; set; }
        public DbSet<ClassBoots.Models.Lecture> Lecture { get; set; }
        public DbSet<ClassBoots.Models.Institution> Institution { get; set; }
        public DbSet<ClassBoots.Models.Group> Group { get; set; }
        public DbSet<ClassBoots.Models.AppUser> AppUser { get; set; }
    }
}
