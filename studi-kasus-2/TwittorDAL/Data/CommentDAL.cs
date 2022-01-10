using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using TwittorDAL.Dtos;
using TwittorDAL.Helpers;
using TwittorDAL.Models;

namespace TwittorDAL.Data
{
  public class CommentDAL
  {
    private readonly string _connString;

    public CommentDAL(IConfiguration configuration)
    {
      _connString = configuration.GetConnectionString("LocalConnection");
    }

    public async Task<Boolean> AddComment(CommentInput input)
    {
      try
      {
        using (var context = new AppDbContext(_connString))
        {
          var result = context.Users.Where(u => u.Id == input.UserID && u.Lock == false).SingleOrDefault();
          if (result == null) return false;
          var result2 = context.TwittorModels.Where(t => t.Id == input.TwittorModelID).SingleOrDefault();
          if (result2 == null) return false;
          var comment = new Comment
          {
            User = result,
            TwittorModel = result2,
            Description = input.Description,
            CreatedAt = DateTime.Now,

          };
          await context.Comments.AddAsync(comment);
          await context.SaveChangesAsync();
          return true;
        }
      }
      catch (System.Exception ex)
      {
        LoggingConsole.Log(ex.Message);
        return false;
      }
    }

  }
}