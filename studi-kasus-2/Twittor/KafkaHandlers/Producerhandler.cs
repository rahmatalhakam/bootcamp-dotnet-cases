using System;
using System.Net;
using System.Threading.Tasks;
using Confluent.Kafka;
using Twittor.Constants;
using Twittor.Helper;
using Twittor.Helpers;

namespace Twittor.KafkaHandlers
{
  public class Producerhandler
  {
    public static async Task<Boolean> ProduceMessage(string _topic, KafkaConfig _config, string key, string value)
    {
      var isSucceed = false;
      var topic = _topic;
      var config = new ProducerConfig
      {
        BootstrapServers = _config.BootstrapServers,
        ClientId = Dns.GetHostName(),
      };
      using (var producer = new ProducerBuilder<string, string>(config).Build())
      {

        producer.Produce(topic, new Message<string, string>
        {
          Key = key,
          Value = value
        }, (deliveryReport) =>
        {
          if (deliveryReport.Error.Code != ErrorCode.NoError)
          {
            LoggingConsole.Log($"Failed to deliver message: {deliveryReport.Error.Reason}");
          }
          else
          {
            LoggingConsole.Log($"Produced message to: {deliveryReport.TopicPartitionOffset} Key: {key} Value: {value}");
            isSucceed = true;
          }
        });
        producer.Flush(TimeSpan.FromSeconds(10));

      }
      return await Task.FromResult(isSucceed);
    }
  }
}