using System;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using TwittorDAL.Controllers;
using TwittorDAL.Dtos;

namespace TwittorDAL.Handlers
{
  public class ConsumerHandler
  {
    private readonly IConfiguration _iconfiguration;

    public ConsumerHandler(IConfiguration configuration)
    {
      _iconfiguration = configuration;
    }

    public Task StartAsync()
    {
      //add semua thread yaitu cukup user, twittor, dan commnet
      Thread userThread = new Thread(UserTopicListener)
      {
        Name = "UserThread"
      };

      userThread.Start();


      return Task.CompletedTask;
    }

    public void UserTopicListener()
    {
      var server = _iconfiguration.GetSection("ConsumerConfig:BootstrapServers").Value;
      var groupId = _iconfiguration.GetSection("ConsumerConfig:GroupId").Value;
      var config = new ConsumerConfig
      {
        BootstrapServers = server,
        GroupId = groupId,
        AutoOffsetReset = AutoOffsetReset.Earliest
      };
      var topic = "twittor-user-topic";
      CancellationTokenSource cts = new CancellationTokenSource();
      Console.CancelKeyPress += (_, e) =>
      {
        e.Cancel = true; // prevent the process from terminating.
        cts.Cancel();
      };
      using (var consumer = new ConsumerBuilder<string, string>(config).Build())
      {
        Console.WriteLine("Connected");
        consumer.Subscribe(topic);
        try
        {
          while (true)
          {
            var cr = consumer.Consume(cts.Token);
            Console.WriteLine($"Consumed record with key: {cr.Message.Key} and value: {cr.Message.Value}");
            //tambahin switch case utk setiap event key yang berbeda
            UsersController usersController = new UsersController(_iconfiguration);
            switch (cr.Message.Key)
            {
              case "register":
                RegisterInput deserialized = JsonSerializer.Deserialize<RegisterInput>(cr.Message.Value);
                usersController.Registration(deserialized);
                break;
              default:
                break;
            }
          }
        }
        catch (OperationCanceledException)
        {
          // Ctrl-C was pressed.
        }
        finally
        {
          consumer.Close();
        }
      }
    }
  }

}