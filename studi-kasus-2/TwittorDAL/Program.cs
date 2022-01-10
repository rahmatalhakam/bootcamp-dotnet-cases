using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using TwittorDAL.Controllers;
using TwittorDAL.Dtos;
using TwittorDAL.Handlers;

namespace TwittorDAL
{
  class Program
  {
    private static IConfigurationRoot _iconfiguration;

    static async Task Main(string[] args)
    {
      GetAppSettingsFile();
      await TopicInitHandler.TopicInit(_iconfiguration);
      ConsumerHandler consumerHandler = new ConsumerHandler(_iconfiguration);
      await consumerHandler.StartAsync();
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
