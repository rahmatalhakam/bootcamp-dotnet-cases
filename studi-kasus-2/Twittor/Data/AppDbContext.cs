using Microsoft.EntityFrameworkCore;
using Twittor.Models;

namespace Twittor.Data
{
  public class AppDbContext : DbContext
  {
    public AppDbContext(DbContextOptions opt) : base(opt)
    {

    }

    public DbSet<Comment> Comments { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<TwittorModel> TwittorModels { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<User>()
          .HasIndex(p => new { p.Email, p.Username })
          .IsUnique(true);
    }
  }
}