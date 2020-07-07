using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YappiesTesting.Models;

namespace YappiesTesting.Data
{
    public class yappiesTestingContext : DbContext
    {
        public yappiesTestingContext (DbContextOptions<yappiesTestingContext> options)
            : base(options)
        {
        }

        public DbSet<YappiesTesting.Models.Activity> Activities { get; set; }

        public DbSet<YappiesTesting.Models.Parent> Parents { get; set; }

        public DbSet<YappiesTesting.Models.Program> Programs { get; set; }

        public DbSet<YappiesTesting.Models.ProgramSupervisor> ProgramSupervisors { get; set; }

        public DbSet<YappiesTesting.Models.Program_Parent> Programs_Parents { get; set; }

        public DbSet<YappiesTesting.Models.Announcement> Announcements { get; set; }

        public DbSet<YappiesTesting.Models.Child> Children { get; set; }

        public DbSet<YappiesTesting.Models.Program_Child> Programs_Children { get; set; }

        public DbSet<YappiesTesting.Models.Conversation> Conversations { get; set; }

        public DbSet<YappiesTesting.Models.Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("YT");

            //Prevent Cascade Delete from Program Supervisor to Program
            modelBuilder.Entity<ProgramSupervisor>()
                .HasMany<Models.Program>(s => s.Programs)
                .WithOne(p => p.ProgramSupervisor)
                .HasForeignKey(p => p.ProgramSupervisorID)
                .OnDelete(DeleteBehavior.Restrict);

            //Prevent Cascade Delete from Program to Activity
            modelBuilder.Entity<Models.Program>()
                .HasMany<Activity>(p => p.Activities)
                .WithOne(a => a.Program)
                .HasForeignKey(a => a.ProgramID)
                .OnDelete(DeleteBehavior.Restrict);

            //Many to Many Intersection
            modelBuilder.Entity<Program_Parent>()
                .HasKey(t => new { t.ProgramID, t.ParentID });

            //Many to Many Intersection
            modelBuilder.Entity<Program_Child>()
                .HasKey(t => new { t.ProgramID, t.ChildID });

            //Many to Many Intersection
            modelBuilder.Entity<Conversation>()
                .HasKey(t => new { t.ParentID, t.ProgramSupervisorID });
        }
    }
}
