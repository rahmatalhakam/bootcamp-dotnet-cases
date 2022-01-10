using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace TwittorDAL
{
  class Program
  {
    private static IConfigurationRoot _iconfiguration;

    static void Main(string[] args)
    {
      Console.WriteLine("Hello World!");
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
