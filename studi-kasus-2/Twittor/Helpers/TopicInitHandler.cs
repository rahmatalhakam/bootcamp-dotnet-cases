using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Confluent.Kafka;
using Confluent.Kafka.Admin;
using GraphQLAuth.Helper;
using HotChocolate;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Twittor.Constants;
using Twittor.Helpers;

namespace Twittor.Handlers
{
  public class TopicInitHandler
  {
    public static async Task TopicInit([Service] IOptions<KafkaConfig> configuration)
    {

      var config = new ProducerConfig
      {
        BootstrapServers = configuration.Value.BootstrapServers,
        ClientId = Dns.GetHostName(),
      };
      using (var adminClient = new AdminClientBuilder(config).Build())
      {
        List<string> topics = new List<string>();
        topics.Add(TopicList.CommentTopic);
        topics.Add(TopicList.UserTopic);
        topics.Add(TopicList.TwittorTopic);
        topics.Add(TopicList.LoggingTopic);
        foreach (var topic in topics)
        {
          try
          {
            await adminClient.CreateTopicsAsync(new List<TopicSpecification> {
                new TopicSpecification {
                    Name = topic,
                    NumPartitions = configuration.Value.NumPartitions,
                    ReplicationFactor = configuration.Value.ReplicationFactor
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