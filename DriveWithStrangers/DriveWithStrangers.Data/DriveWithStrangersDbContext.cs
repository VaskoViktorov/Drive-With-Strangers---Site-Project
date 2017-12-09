using Microsoft.AspNetCore.Identity;

namespace DriveWithStrangers.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class DriveWithStrangersDbContext : IdentityDbContext<User>
    {
        public DriveWithStrangersDbContext(DbContextOptions<DriveWithStrangersDbContext> options)
            : base(options)
        {
        }

        public DbSet<Article> Articles { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Trip> Trips { get; set; }

        public DbSet<UserTrip> UserTrips { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Article>()
                .HasOne(a => a.User)
                .WithMany(u => u.Articles)
                .HasForeignKey(a => a.UserId);

            builder
                .Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId);

            builder
                .Entity<Comment>()
                .HasOne(c => c.Trip)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.TripId);

            builder
                .Entity<Trip>()
                .HasOne(t => t.Driver)
                .WithMany(u => u.MyTrips)
                .HasForeignKey(t => t.DriverId);

            builder
                .Entity<UserTrip>()
                .HasKey(ut => new {ut.UserId, ut.TripId});

            builder
                .Entity<UserTrip>()
                .HasOne(ut => ut.User)
                .WithMany(u => u.Trips)
                .HasForeignKey(ut => ut.UserId);

            builder
                .Entity<UserTrip>()
                .HasOne(ut => ut.Trip)
                .WithMany(u => u.Passengers)
                .HasForeignKey(ut => ut.TripId);

            base.OnModelCreating(builder);            
        }
    }
}
