using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cryptotracker.entity;
using Microsoft.EntityFrameworkCore;

namespace cryptotracker.dataaccess.Concrete.EF
{   
    public class CTrackerContext:DbContext
    {
        public DbSet<User> Users {get; set;}
        public DbSet<CryptoCurrency> Favorites {get; set;}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source = DESKTOP-UQQU5HJ;Initial Catalog=cryptoTrackerDb;Integrated Security=SSPI;TrustServerCertificate=True");
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(e => e.UserId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<User>()
            .HasMany(e => e.Favorites)
            .WithOne(e => e.User)
            .HasForeignKey(e=>e.Id)
            .IsRequired();
        }
    }
}