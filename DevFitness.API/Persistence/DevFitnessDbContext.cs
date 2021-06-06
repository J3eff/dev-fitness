﻿using DevFitness.API.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevFitness.API.Persistence
{
    public class DevFitnessDbContext : DbContext
    {
        public DevFitnessDbContext(DbContextOptions<DevFitnessDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Meal> Meals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(e =>
            {
                e.HasKey(u => u.Id);

                e.HasMany(u => u.Maels)
                    .WithOne()
                    .HasForeignKey(m => m.UserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Meal>(e =>
            {
                e.HasKey(m => m.Id);
            });
        }
    }
}
