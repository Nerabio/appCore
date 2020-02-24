using System;
using System.Collections.Generic;
using System.Text;
using Common.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace DataAccess.Context
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Device> Devices { get; set; }
        public DbSet<DeviceType> DeviceType { get; set; }
        public DbSet<SectionKey> SectionKeys { get; set; }
        public DbSet<Key> Keys { get; set; }
        public DbSet<TypeKey> TypeKeys { get; set; }
        public DbSet<TypeKeyValue> TypeKeyValues { get; set; }


        //public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        //    : base(options)
        //{
        //}

        public ApplicationDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=helloappdb;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SectionKey>()
            .HasOne<Device>(d => d.Device)
            .WithMany(ad => ad.SectionKey)
            .HasForeignKey(s => s.DeviceId);

            modelBuilder.Entity<Key>()
            .HasOne<SectionKey>(k => k.SectionKey)
            .WithMany(sk => sk.Keys)
            .HasForeignKey(k => k.SectionKeyId);

            modelBuilder.Entity<Key>()
            .HasOne<TypeKey>(k => k.TypeKey)
            .WithOne(tk => tk.Key)
            .HasForeignKey<Key>(k => k.TypeKeyId);

            modelBuilder.Entity<Key>()
            .HasOne<TypeKeyValue>(k => k.TypeKeyValue)
            .WithOne(tv => tv.Key)
            .HasForeignKey<Key>(k => k.TypeKeyValueId);
        }
    }
}
