using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EventStoreApp.Models;
using EventStoreApp.Models.Entities;

namespace EventStoreApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<Amenity> Amenities { get; set; } 

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Event>()
                .HasMany(a => a.Amenities)
                .WithOne();
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            // builder.Entity<EventAmenity>().HasKey(t => new {t.EventId, t.AmenityId});
            // builder.Entity<EventAmenity>()
            //     .HasOne(pt => pt.Event)
            //     .WithMany(p => p.EventAmenities)
            //     .HasForeignKey(pt => pt.EventId);

            // builder.Entity<EventAmenity>()
            //     .HasOne(pt => pt.Amenity)
            //     .WithMany(p => p.EventAmenities)
            //     .HasForeignKey(pt => pt.AmenityId);


        }
    }
}
