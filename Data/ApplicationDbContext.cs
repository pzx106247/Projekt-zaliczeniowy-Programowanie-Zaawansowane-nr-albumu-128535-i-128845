using Esport.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Esport.Data;

public class ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options
) : IdentityDbContext(options) {
    public DbSet<Game> Games { get; set; }
    public DbSet<Tournament> Tournaments { get; set; }
    public DbSet<Participant> Participants { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<IdentityUser>().HasData(
            new IdentityUser {
                Id = "1",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@example.com",
                NormalizedEmail = "admin@example.com",
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null!, "admin")
            },
            new IdentityUser {
                Id = "2",
                UserName = "Nowak",
                NormalizedUserName = "Nowak",
                Email = "nowak@example.com",
                NormalizedEmail = "nowak@example.com",
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null!, "test1234")
            },
            new IdentityUser {
                Id = "3",
                UserName = "Kowalski",
                NormalizedUserName = "Kowalski",
                Email = "kowalski@example.com",
                NormalizedEmail = "kowalski@example.com",
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null!, "test1234")
            }
        );

        modelBuilder.Entity<IdentityRole>().HasData(
            new IdentityRole { Id = "2", Name = "Admin" }
        );

        modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string> { UserId = "1", RoleId = "2" }
        );

        modelBuilder.Entity<Game>().HasData(
            new Game { Id = Guid.NewGuid(), Name = "League of Legends", Genre = "MOBA" },
            new Game { Id = Guid.NewGuid(), Name = "Counter-Strike: Global Offensive", Genre = "FPS" },
            new Game { Id = Guid.NewGuid(), Name = "Valorant", Genre = "FPS" }
        );

        modelBuilder.Entity<Participant>(entity => {
            entity.HasKey(e => new { e.TournamentId, e.UserId });
            entity.HasOne(e => e.Tournament).WithMany().HasForeignKey(e => e.TournamentId);
            entity.HasOne(e => e.User).WithMany().HasForeignKey(e => e.UserId);
        });

        modelBuilder.Entity<Tournament>(entity => {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired();
            entity.Property(e => e.Date).IsRequired();
            entity.Property(e => e.Approved).IsRequired();
            entity.HasOne(e => e.Owner).WithMany().HasForeignKey(e => e.OwnerId);
            entity.HasOne(e => e.Game).WithMany().HasForeignKey(e => e.GameId);
        });
    }
}
