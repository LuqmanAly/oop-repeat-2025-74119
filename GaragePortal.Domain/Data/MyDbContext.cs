using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using GaragePortal.Domain.Entities;

namespace GaragePortal.Domain.Data

{
    public partial class MyDbContext : DbContext
    {
        public MyDbContext()
        {
        }

        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Admin> Admins { get; set; } = null!;

        public virtual DbSet<Car> Cars { get; set; } = null!;
        public virtual DbSet<Mechanic> Mechanics { get; set; } = null!;
        public virtual DbSet<Service> Services { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=GaragePortal;Username=postgres;Password=postgres");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);



            // Configure Customer entity
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.ClientName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ClientEmail)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            // Configure Car entity
            modelBuilder.Entity<Car>(entity =>
            {
                entity.Property(e => e.LicensePlate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                // Relationships
                entity.HasOne(c => c.Client)
                      .WithMany(cu => cu.Vehicles)
                      .HasForeignKey(c => c.ClientId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Configure Mechanic entity
            modelBuilder.Entity<Mechanic>(entity =>
            {
                entity.Property(e => e.TechnicianName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            // Configure Service entity
            modelBuilder.Entity<Service>(entity =>
            {
                entity.Property(e => e.RepairDescription)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                // Relationships
                entity.HasOne(s => s.Vehicle)
                      .WithMany(c => c.Repairs)
                      .HasForeignKey(s => s.VehicleId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(s => s.Technician)
                      .WithMany(m => m.Repairs)
                      .HasForeignKey(s => s.TechnicianId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
