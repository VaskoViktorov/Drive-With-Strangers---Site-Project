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

       public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
