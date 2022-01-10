using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Confluent.Kafka;
using Confluent.Kafka.Admin;
using Microsoft.Extensions.Configuration;
using TwittorDAL.Helpers;

namespace TwittorDAL.Handlers
{
  public class TopicInitHandler
  {
    public static async Task TopicInit(IConfiguration configuration)
    {

      var config = new ProducerConfig
      {
        BootstrapServers = configuration.GetSection("ConsumerConfig:BootstrapServers").Value,
        ClientId = Dns.GetHostName(),
      };
      using (var adminClient = new AdminClientBuilder(config).Build())
      {
        List<string> topics = new List<string>();
        topics.Add(TopicList.CommentTopic);
        topics.Add(TopicList.UserTopic);
        topics.Add(TopicList.TwittorTopic);
        foreach (var topic in topics)
        {
          try
          {
            await adminClient.CreateTopicsAsync(new List<TopicSpecification> {
                new TopicSpecification {
                    Name = topic,
                    NumPartitions = Int16.Parse(configuration["NumPartitions"]),
                    ReplicationFactor = Int16.Parse(configuration["ReplicationFactor"])
                } });
            LoggingConsole.Log($"Topic {topic} created successfully");
          }
          catch (CreateTopicsException e)
          {
            if (e.Results[0].Error.Code != ErrorCode.TopicAlreadyExists)
            {
              LoggingConsole.Log($"An error occured creating topic {topic}: {e.Results[0].Error.Reason}");
            }
            else
            {
              LoggingConsole.Log($"Topic {topic} already exists");
            }
          }
        }

      }
    }
  }
}