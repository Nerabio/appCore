using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartHouse.Entities;

namespace SmartHouse.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Device> Devices { get; set; }
        public DbSet<DeviceType> DeviceType { get; set; }

        public DbSet<Commands> Commands { get; set; }
        public DbSet<Keys> Keys { get; set; }

        public DbSet<Values> Values { get; set; }
        public DbSet<TypeKey> TypeKey { get; set; }
        


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
