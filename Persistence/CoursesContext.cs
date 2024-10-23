using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Dominion;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Persistence
{
    public class CoursesContext: IdentityDbContext<User>
    {
        public CoursesContext(DbContextOptions options) : base(options){

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CourseTeacher>().HasKey(ct => new{ct.TeacherId, ct.CourseId});

            modelBuilder.Entity<Course>()
                        .HasOne(c => c.SalesPrice)  // Relación uno a uno
                        .WithOne(p => p.Course)     // Inversa
                        .HasForeignKey<Price>(p => p.CourseId);  // Clave externa
        }

        public DbSet<Comentary> Comentary   { get; set; }
        public DbSet<Course> Course { get; set; }
        public DbSet<Teacher> Teacher { get; set; }
        public DbSet<CourseTeacher> CourseTeacher { get; set;}
        public DbSet<Price> Price { get; set; }

    }
}