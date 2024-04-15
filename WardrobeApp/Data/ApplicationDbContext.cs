using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.Policy;
using WardrobeApp.Models;

namespace WardrobeApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ClothingItem> ClothingItems { get; set; }
        public DbSet<Outfit> Outfits { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<AppUser>().ToTable("Users");
            var hasher = new PasswordHasher<AppUser>();
            var adminId = Guid.NewGuid();
            var adminRoleId = Guid.NewGuid();

            modelBuilder.Entity<Outfit>()
                .HasOne(o => o.Headwear)
                .WithMany()
                .HasForeignKey(o => o.HeadwearId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Outfit>()
                .HasOne(o => o.Top)
                .WithMany()
                .HasForeignKey(o => o.TopId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Outfit>()
                .HasOne(o => o.Bottom)
                .WithMany()
                .HasForeignKey(o => o.BottomId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Outfit>()
                .HasOne(o => o.Footwear)
                .WithMany()
                .HasForeignKey(o => o.FootwearId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Outfit>()
                .HasOne(o => o.Accessory)
                .WithMany()
                .HasForeignKey(o => o.AccessoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<IdentityRole<Guid>>().HasData(new IdentityRole<Guid>
            {
                Id = adminRoleId,
                Name = "Admin",
                NormalizedName = "ADMIN"
            });
            modelBuilder.Entity<AppUser>().HasData(new AppUser
            {
                Id = adminId,
                UserName = "admin@email.com",
                NormalizedUserName = "ADMIN@EMAIL.COM",
                Email = "admin@email.com",
                NormalizedEmail = "ADMIN@EMAIL.COM",
                EmailConfirmed = true,
                SecurityStamp = "something",
                PasswordHash = hasher.HashPassword(new AppUser(), "supertajneheslo"),
                Nick = "Admin"
            });
            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = adminRoleId,
                UserId = adminId
            });
        }
    }
}
