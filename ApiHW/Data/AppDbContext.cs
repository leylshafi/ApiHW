﻿using ApiHW.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiHW.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions options):base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().Property(c => c.Name).IsRequired().HasMaxLength(70);
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
