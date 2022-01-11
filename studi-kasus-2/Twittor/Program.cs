using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Twittor.Data;
using Twittor.Handlers;
using Twittor.Helper;

namespace Twittor
{
  public class Program
  {
    private static IConfigurationRoot _iconfiguration;

    public static async Task Main(string[] args)
    {
      var host = CreateHostBuilder(args).Build();
      CreatedDbIfNotExists(host);
      GetAppSettingsFile();
      await TopicInitHandler.TopicInit(_iconfiguration);
      host.Run();
    }
    private static void CreatedDbIfNotExists(IHost host)
    {
      using (var scope = host.Services.CreateScope())
      {
        var services = scope.ServiceProvider;
        try
        {
          var context = services.GetRequiredService<AppDbContext>();
          DbInitializer.Initialize(context);
          var config = services.GetService<KafkaConfig>();

        }
        catch (Exception ex)
        {
          var logger = services.GetRequiredService<ILogger<Program>>();
          logger.LogError(ex, "Terjadi error ketika membuat database.");
        }
      }
    }

    private static void GetAppSettingsFile()
    {
      var builder = new ConfigurationBuilder()
                           .SetBasePath(Directory.GetCurrentDirectory())
                           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
      _iconfiguration = builder.Build();
    }
    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
              webBuilder.UseStartup<Startup>();
            });
  }
}
