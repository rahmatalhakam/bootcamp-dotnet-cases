using System;
using Microsoft.Extensions.Configuration;
using TwittorDAL.Data;
using TwittorDAL.Dtos;

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
      if (result.Result) Console.WriteLine("Registration added");
      else Console.WriteLine("Registration failed");
    }
  }
}