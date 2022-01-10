using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using TwittorDAL.Dtos;
using TwittorDAL.Helpers;
using TwittorDAL.Models;

namespace TwittorDAL.Data
{
  public class UserDAL
  {
    // TODO: 1. UpdateUser 2. ChangePassword 3. LockUser 4. ChangeUserRole
    // TODO: 5. AddRoleForUser

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
          // var result = context.Users.Where(u => u.Username == userInput.Username || u.Email == userInput.Email).SingleOrDefault();
          // if (result != null) return false;
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
          await context.SaveChangesAsync();
          if (!await AddRoleForUser(new UserRoleInput { RoleId = 2, UserId = user.Id })) return false;
          return true;
        }
      }
      catch (Exception ex)
      {
        LoggingConsole.Log(ex.Message);
        return false;
      }
    }

    public async Task<Boolean> UpdatePasword(UpdatePassInput input)
    {
      try
      {
        using (var context = new AppDbContext(_connString))
        {
          var result = context.Users.Where(u => u.Username == input.Username && u.Lock == false).SingleOrDefault();
          if (result == null) return false;
          if (ComputeHash.ComputeSha256HashFunc(input.OldPassword) != result.Password) return false;
          result.Password = ComputeHash.ComputeSha256HashFunc(input.NewPassword);
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

    public async Task<Boolean> LockUser(LockUserInput input)
    {
      try
      {
        using (var context = new AppDbContext(_connString))
        {
          var result = context.Users.Where(u => u.Username == input.Username).SingleOrDefault();
          if (result == null) return false;
          result.Lock = input.Lock;
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

    public async Task<Boolean> AddRoleForUser(UserRoleInput input)
    {
      try
      {
        using (var context = new AppDbContext(_connString))
        {
          var result3 = context.UserRoles.Where(ur => ur.RoleId == input.RoleId && ur.UserId == input.UserId).SingleOrDefault();
          if (result3 != null) return false;
          await context.UserRoles.AddAsync(new UserRole { RoleId = input.RoleId, UserId = input.UserId });
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

    public async Task<Boolean> UpdateRoleForUser(UserRoleUpdate input)
    {
      try
      {
        using (var context = new AppDbContext(_connString))
        {
          var result3 = context.UserRoles.Where(ur => ur.RoleId == input.OldRoleId && ur.UserId == input.UserId).SingleOrDefault();
          if (result3 == null) return false;
          if (result3.RoleId == input.NewRoleId) return false;
          result3.RoleId = input.NewRoleId;
          result3.UserId = input.UserId;
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

    public async Task<Boolean> UpdateProfile(ProfileInput input)
    {
      try
      {

        using (var context = new AppDbContext(_connString))
        {
          var result = context.Users.Where(u => u.Id == input.Id && u.Lock == false).SingleOrDefault();
          if (result == null) return false;
          result.Email = input.Email;
          result.FirstName = input.FirstName;
          result.LastName = input.LastName;
          result.Username = input.Username;
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