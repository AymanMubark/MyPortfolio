using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Owner>().Property(owner => owner.Id).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<PortfolioItem>().Property(portfolioItem => portfolioItem.Id).HasDefaultValueSql("NEWID()");

            modelBuilder.Entity<Owner>().HasData(new Owner
            {
                Id = Guid.NewGuid(),
                Avatar = "avata.png",
                FullName = "Khaled ESSAADANI",
                Profil = ".Net Developer"
            });
        }
        public DbSet<Owner> Owner { get; set; }
        public DbSet<PortfolioItem> PortfolioItems { get; set; }

    }
}
