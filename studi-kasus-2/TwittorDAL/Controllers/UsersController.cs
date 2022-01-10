using System;
using Microsoft.Extensions.Configuration;
using TwittorDAL.Data;
using TwittorDAL.Dtos;
using TwittorDAL.Helpers;

namespace TwittorDAL.Controllers
{
  public class UsersController
  {
    private IConfiguration _iconfiguration;

    public UsersController(IConfiguration iconfiguration)
    {
      _iconfiguration = iconfiguration;
    }
    public void Registration(RegisterInput registerInput)
    {
      UserDAL user = new UserDAL(_iconfiguration);
      var result = user.Registration(registerInput);
      if (result.Result) LoggingConsole.Log("Registration succeded");
      else LoggingConsole.Log("Registration failed");
    }

    public void UpdateProfile(ProfileInput profileInput)
    {
      UserDAL user = new UserDAL(_iconfiguration);
      var result = user.UpdateProfile(profileInput);
      if (result.Result) LoggingConsole.Log("Update profile succeded");
      else LoggingConsole.Log("Update profile failed");
    }

    public void UpdatePassword(UpdatePassInput updatePassInput)
    {
      UserDAL user = new UserDAL(_iconfiguration);
      var result = user.UpdatePasword(updatePassInput);
      if (result.Result) LoggingConsole.Log("Change password succeded");
      else LoggingConsole.Log("Change password failed");
    }

    public void LockUser(LockUserInput lockUserInput)
    {
      UserDAL user = new UserDAL(_iconfiguration);
      var result = user.LockUser(lockUserInput);
      if (result.Result) LoggingConsole.Log("update lock user succeded");
      else LoggingConsole.Log("update lock user failed");
    }

    public void AddRoleForUser(UserRoleInput roleInput)
    {
      UserDAL user = new UserDAL(_iconfiguration);
      var result = user.AddRoleForUser(roleInput);
      if (result.Result) LoggingConsole.Log("add role for user succeded");
      else LoggingConsole.Log("add role for user failed");
    }

    public void UpdateRoleForUser(UserRoleUpdate roleInput)
    {
      UserDAL user = new UserDAL(_iconfiguration);
      var result = user.UpdateRoleForUser(roleInput);
      if (result.Result) LoggingConsole.Log("update role user succeded");
      else LoggingConsole.Log("update role user failed");
    }
  }
}