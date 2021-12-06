using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4_Base.Models
{
    public class CustomDbContext : DbContext
    {
        public CustomDbContext(DbContextOptions<CustomDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }
        public DbSet<CustomUser> CustomUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomUser>().HasData(new CustomUser { 
                Id = 1,
                UserName = "rahibjafar",
                Email = "rahib@gmail.com",
                Password = "password",
                City = "Saatli"
            },
            new CustomUser {
                Id = 2,
                UserName = "test",
                Email = "test@gmail.com",
                Password = "password",
                City = "Baku"
            },
            new CustomUser
            {
                Id = 3,
                UserName = "testuser",
                Email = "testuser@gmail.com",
                Password = "password",
                City = "Quba"
            }
            );
            base.OnModelCreating(modelBuilder);
        }
    }
}
