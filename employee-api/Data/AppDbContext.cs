using Microsoft.EntityFrameworkCore;
using employee_api.Models;
using System.Collections.Generic;
using System.Data.Common;


namespace employee_api.Data
{
    public class AppDbContext : DbContext //Connection to the Database. Blackbox....
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {           
        }
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<WorkPattern> WorkPattern { get; set; }
        public DbSet<Absent> Absent { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite(@"Data Source=DB.db");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WorkPattern>()
                .HasMany(s => s.Parts)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<WorkPattern>()
                .HasOne(x=>x.User)
                .WithMany(x=>x.WorkPatterns)
                .HasForeignKey(x=>x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

        }

    }
}

