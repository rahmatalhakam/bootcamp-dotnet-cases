using System.Linq;
using HotChocolate;
using Twittor.Data;
using Twittor.Dtos;
using Twittor.Models;

namespace Twittor.GraphQL
{
  public class Query
  {
    /** TODO:
    1. show all twits
    2. show profile
    */
    public IQueryable<TwotOutput> GetTwots([Service] AppDbContext context) =>
            context.TwittorModels.Select(p => new TwotOutput()
            {
              Id = p.Id,
              Description = p.Description,
              CreatedAt = p.CreatedAt,
              UserID = p.User.Id
            });

    public IQueryable<ProfileOutput> GetProfiles(int userID, [Service] AppDbContext context) =>
            context.Users.Where(u => u.Id == userID).Select(p => new ProfileOutput()
            {
              Id = p.Id,
              Email = p.Email,
              FirstName = p.FirstName,
              LastName = p.LastName,
              Username = p.Username
            });

  }
}