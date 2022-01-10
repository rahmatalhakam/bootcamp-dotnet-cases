using System;
using System.IO;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using TwittorDAL.Controllers;
using TwittorDAL.Dtos;
using TwittorDAL.Handlers;

namespace TwittorDAL
{
  class Program
  {
    private static IConfigurationRoot _iconfiguration;

    static void Main(string[] args)
    {
      GetAppSettingsFile();
      //   ConsumerHandler consumerHandler = new ConsumerHandler(_iconfiguration);
      //   consumerHandler.StartAsync();
      var register = new UserRoleUpdate
      {
        OldRolesId = 2,
        NewRolesId = 1,
        UsersId = 3
      };
      UsersController usersController = new UsersController(_iconfiguration);
      usersController.UpdateRoleForUser(register);
    }
    private static void GetAppSettingsFile()
    {
      var builder = new ConfigurationBuilder()
                           .SetBasePath(Directory.GetCurrentDirectory())
                           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
      _iconfiguration = builder.Build();
    }
  }


}
