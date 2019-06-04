using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wolverine.Core;

namespace Wolverine.Service.Model
{
    public class ProjectContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<SortSession> SortSessions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source = Wolverine.db").EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Card>().Property(x => x.Id).ValueGeneratedOnAddOrUpdate();
            //modelBuilder.Entity<Group>().Property(x => x.Id).ValueGeneratedOnAddOrUpdate();
            //modelBuilder.Entity<Project>().Property(x => x.Id).ValueGeneratedOnAddOrUpdate();

            //modelBuilder.Entity<SortSession>().HasOne(x => x.Project);
            modelBuilder.Entity<Group>().HasMany(x => x.Cards).WithOne().OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Project>().HasMany(x => x.Groups).WithOne().OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<SortSession>().HasOne(x => x.Project).WithMany(y => y.Sessions).OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<Group>().HasOne(x => x.Project).WithMany(x => x.DefaultGroups).OnDelete(DeleteBehavior.Cascade).HasForeignKey(x => x.Project);
            //modelBuilder.Entity<Card>().HasOne(x => x.Group).WithMany(x => x.Cards).OnDelete(DeleteBehavior.Cascade).HasForeignKey(x => x.Group);
        }
    }
}