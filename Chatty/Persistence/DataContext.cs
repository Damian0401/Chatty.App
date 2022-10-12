using Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : IdentityDbContext<ApplicationUser>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Message> Messages { get; set; } = default!;
        public DbSet<Room> Rooms { get; set; } = default!;
        public DbSet<RoomApplicationUser> RoomApplicationUsers { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<RoomApplicationUser>()
                .HasKey(x => new { x.RoomId, x.UserId });

            builder.Entity<RoomApplicationUser>()
                .HasOne(x => x.Room)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.RoomId);

            builder.Entity<RoomApplicationUser>()
                .HasOne(x => x.User)
                .WithMany(x => x.Rooms)
                .HasForeignKey(x => x.UserId);

        }
    }
}