using System;
using System.Collections.Generic;
using System.Text;
using DataAccess.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

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
        public DbSet<DeviceRelation> DeviceRelations { get; set; }
        public DbSet<TaskStatus> TaskStatus { get; set; }
        public DbSet<Task> Tasks { get; set; }
        


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public ApplicationDbContext()
        {
            //Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies()
                .ConfigureWarnings(warnings => warnings.Ignore(CoreEventId.DetachedLazyLoadingWarning))
                .UseSqlServer("Data Source=HELIOS;Initial Catalog=Devices;Persist Security Info=True;User ID=sa;Password=saadmin");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Device>()
                .HasMany<SectionKey>(d => d.SectionKey)
                .WithOne(sk => sk.Device)
                .HasForeignKey(s => s.DeviceId);

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

            //modelBuilder.Entity<DeviceRelation>().HasKey(sc => new { sc.DeviceInId, sc.KeyInId, sc.DeviceOutId, sc.KeyOutId });
            modelBuilder.Entity<Device>()
                .HasMany<DeviceRelation>(d => d.RelationIn)
                .WithOne(dr => dr.DeviceIn)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(dr => dr.DeviceInId);

            modelBuilder.Entity<Device>()
                .HasMany<DeviceRelation>(d => d.RelationOut)
                .WithOne(dr => dr.DeviceOut)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(dr => dr.DeviceOutId);

            modelBuilder.Entity<Key>()
                .HasMany<DeviceRelation>(k => k.RelationIn)
                .WithOne(dr => dr.KeyIn)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(dr => dr.KeyInId);

            modelBuilder.Entity<Key>()
                .HasMany<DeviceRelation>(k => k.RelationOut)
                .WithOne(dr => dr.KeyOut)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(dr => dr.KeyOutId);

            //modelBuilder.Entity<Key>()
            //    .Property(k => k.DateCreated)
            //    .ValueGeneratedOnUpdate();

            modelBuilder.Entity<Task>()
                .HasOne<TaskStatus>(t => t.TaskStatus)
                .WithOne(ts => ts.Task)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey<Task>(t => t.TaskStatusId);

            modelBuilder.Entity<Device>()
                .HasMany<Task>(d => d.Tasks)
                .WithOne(t => t.Device)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(t => t.DeviceId);


            modelBuilder.Entity<SectionKey>()
                .HasMany<Task>(sk => sk.Tasks)
                .WithOne(t => t.SectionKey)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(t => t.SectionKeyId);
        }
    }
}
