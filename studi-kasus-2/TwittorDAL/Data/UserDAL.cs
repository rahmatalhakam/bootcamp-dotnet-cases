using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using TwittorDAL.Dtos;
using TwittorDAL.Helper;
using TwittorDAL.Models;

namespace TwittorDAL.Data
{
  public class UserDAL
  {
    private readonly string _connString;

    public UserDAL(IConfiguration configuration)
    {
      _connString = configuration.GetConnectionString("LocalConnection");
    }

    public async Task<Boolean> Registration(RegisterInput userInput)
    {
      try
      {
        using (var context = new AppDbContext(_connString))
        {
          var user = new User
          {
            Username = userInput.Username,
            Email = userInput.Email,
            Password = ComputeHash.ComputeSha256HashFunc(userInput.Password),
            FirstName = userInput.FirstName,
            LastName = userInput.LastName,
            CreatedAt = System.DateTime.Now,
            Lock = false
          };
          await context.Users.AddAsync(user);
          var result = await context.SaveChangesAsync();
        }
        return true;
      }
      catch (Exception ex)
      {
        throw new Exception($"{ex.Message}");
      }
    }


  }
}