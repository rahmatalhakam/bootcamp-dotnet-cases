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
          var result = context.Users.Where(u => u.Username == userInput.Username || u.Email == userInput.Email).SingleOrDefault();
          if (result != null) return false;
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
          if (!await AddRoleForUser(new UserRoleInput { RolesId = 2, UsersId = user.Id })) return false;
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
          var result = context.Roles.Where(u => u.Id == input.RolesId).SingleOrDefault();
          if (result == null) return false;
          var result2 = context.Users.Where(u => u.Id == input.UsersId).SingleOrDefault(); ;
          if (result == null) return false;
          var result3 = context.UserRoles.Where(ur => ur.RolesId == input.RolesId && ur.UsersId == input.UsersId).SingleOrDefault();
          if (result != null) return false;
          await context.UserRoles.AddAsync(new UserRole { RolesId = input.RolesId, UsersId = input.UsersId });
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
          var result = context.Roles.Where(u => u.Id == input.OldRolesId).SingleOrDefault();
          if (result == null) return false;
          var result2 = context.Users.Where(u => u.Id == input.UsersId).SingleOrDefault(); ;
          if (result == null) return false;
          var result3 = context.UserRoles.Where(ur => ur.RolesId == input.OldRolesId && ur.UsersId == input.UsersId).SingleOrDefault();
          if (result == null) return false;
          if (result3.RolesId == input.NewRolesId) return false;
          result3.RolesId = input.NewRolesId;
          result3.UsersId = input.UsersId;
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