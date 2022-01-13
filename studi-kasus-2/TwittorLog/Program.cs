using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using TwittorLog.Constants;
using TwittorLog.Handlers;
using TwittorLog.Helpers;

namespace TwittorLog
{
  class Program
  {
    private static IConfigurationRoot _iconfiguration;

    public static async Task Main(string[] args)
    {
      GetAppSettingsFile();
      await TopicInitHandler.TopicInit(_iconfiguration);
      var server = _iconfiguration.GetSection("ConsumerConfig:BootstrapServers").Value;
      var groupId = _iconfiguration.GetSection("ConsumerConfig:GroupId").Value;
      var config = new ConsumerConfig
      {
        BootstrapServers = server,
        GroupId = groupId,
        AutoOffsetReset = AutoOffsetReset.Earliest
      };
      var topic = GetTopics();
      CancellationTokenSource cts = new CancellationTokenSource();
      Console.CancelKeyPress += (_, e) =>
      {
        e.Cancel = true; // prevent the process from terminating.
        cts.Cancel();
      };

      using (var consumer = new ConsumerBuilder<string, string>(config).Build())
      {
        LoggingConsole.Log("Connected to TwittorLog");
        consumer.Subscribe(topic);
        try
        {
          while (true)
          {
            var cr = consumer.Consume(cts.Token);
            LoggingConsole.Log($"Consumed record with key: {cr.Message.Key} and value: {cr.Message.Value}");
          }
        }
        catch (OperationCanceledException)
        {
          // Ctrl-C was pressed..
        }
        finally
        {
          consumer.Close();
        }

      }
    }
    private static List<string> GetTopics()
    {
      List<string> list = new List<string>();
      list.Add(TopicList.CommentTopic);
      list.Add(TopicList.LoggingTopic);
      list.Add(TopicList.TwittorTopic);
      list.Add(TopicList.UserTopic);
      return list;
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
