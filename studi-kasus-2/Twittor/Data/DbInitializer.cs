using System.Linq;
using Twittor.Models;
namespace Twittor.Data
{
  public class DbInitializer
  {
    public static void Initialize(AppDbContext context)
    {
      context.Database.EnsureCreated();
      if (context.Roles.Any())
      {
        return;
      }

      var roles = new Role[]{
        new Role{ Name="ADMIN"},
        new Role{ Name="MEMBER"}
      };

      foreach (var role in roles)
      {
        context.Roles.Add(role);
      }
      context.SaveChanges();
    }
  }
}