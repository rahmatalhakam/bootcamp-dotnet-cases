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
      //   var register = new RegisterInput
      //   {
      //     Email = "test@test.com",
      //     FirstName = "Test First",
      //     LastName = "Test last",
      //     Password = "test",
      //     Username = "test"
      //   };
      //   var register = new UserRoleInput
      //   {
      //     RoleId = 2,
      //     UserId = 1
      //   };
      //   var register = new ProfileInput
      //   {
      //     Id = 1,
      //     Email = "test@test.com",
      //     FirstName = "Test First",
      //     LastName = "Test last",
      //     Username = "test"
      //   };
      //   UsersController usersController = new UsersController(_iconfiguration);
      //   usersController.Registration(register);

      //   var twot = new TwotInput
      //   {
      //     UserID = 2,
      //     Description = "this is the tweet😃"
      //   };
      TwittorsController twittorController = new TwittorsController(_iconfiguration);
      twittorController.DeleteTwot(2);

      //   var commentInput = new CommentInput
      //   {
      //     UserID = 2,
      //     Description = "this is the comment👹👹👹",
      //     TwittorModelID = 2,
      //   };

      //   CommentsController commentsController = new CommentsController(_iconfiguration);
      //   commentsController.AddComment(commentInput);
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
