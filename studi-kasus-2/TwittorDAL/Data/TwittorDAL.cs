using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using TwittorDAL.Dtos;
using TwittorDAL.Helpers;
using TwittorDAL.Models;
using Microsoft.EntityFrameworkCore;

namespace TwittorDAL.Data
{
  public class TwittorDALClass
  {
    private readonly string _connString;

    public TwittorDALClass(IConfiguration configuration)
    {
      _connString = configuration.GetConnectionString("LocalConnection");
    }

    public async Task<Boolean> AddTwot(TwotInput input)
    {
      try
      {
        using (var context = new AppDbContext(_connString))
        {
          var result = context.Users.Where(u => u.Id == input.UserID && u.Lock == false).SingleOrDefault();
          if (result == null) return false;
          var twot = new TwittorModel
          {
            User = result,
            Description = input.Description,
            CreatedAt = DateTime.Now,

          };
          await context.TwittorModels.AddAsync(twot);
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

    public async Task<Boolean> DeleteTwot(int TwotID)
    {
      try
      {
        using (var context = new AppDbContext(_connString))
        {
          var result = context.TwittorModels.Where(t => t.Id == TwotID).Include(c => c.Comments).First();
          if (result == null) return false;
          result.Comments.Clear();
          context.TwittorModels.Remove(result);
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