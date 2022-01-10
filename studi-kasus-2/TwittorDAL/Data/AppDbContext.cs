using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TwittorDAL.Models;

namespace TwittorDAL.Data
{
  public class AppDbContext : DbContext
  {
    private readonly string _connString;

    public AppDbContext(string connString)
    {
      _connString = connString;
    }

    public DbSet<Comment> Comments { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<TwittorModel> TwittorModels { get; set; }
    public DbSet<User> Users { get; set; }

    public DbSet<UserRole> UserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseSqlServer(_connString);
    }
  }
}