using System;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using TwittorDAL.Controllers;
using TwittorDAL.Dtos;
using TwittorDAL.Helpers;

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
      Thread twittorThread = new Thread(TwittorTopicListener)
      {
        Name = "TwittorThread"
      };

      Thread commentThread = new Thread(CommentTopicListener)
      {
        Name = "CommentThread"
      };


      userThread.Start();
      twittorThread.Start();
      commentThread.Start();


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
      var topic = TopicList.UserTopic;
      CancellationTokenSource cts = new CancellationTokenSource();
      Console.CancelKeyPress += (_, e) =>
      {
        e.Cancel = true; // prevent the process from terminating.
        cts.Cancel();
      };
      using (var consumer = new ConsumerBuilder<string, string>(config).Build())
      {
        LoggingConsole.Log("Connected to " + topic);
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
              case TopicKeyList.Registration:
                RegisterInput des1 = JsonSerializer.Deserialize<RegisterInput>(cr.Message.Value);
                usersController.Registration(des1);
                break;
              case TopicKeyList.UpdatePassword:
                UpdatePassInput des2 = JsonSerializer.Deserialize<UpdatePassInput>(cr.Message.Value);
                usersController.UpdatePassword(des2);
                break;
              case TopicKeyList.LockUser:
                LockUserInput des3 = JsonSerializer.Deserialize<LockUserInput>(cr.Message.Value);
                usersController.LockUser(des3);
                break;
              case TopicKeyList.AddRoleForUser:
                UserRoleInput des4 = JsonSerializer.Deserialize<UserRoleInput>(cr.Message.Value);
                usersController.AddRoleForUser(des4);
                break;
              case TopicKeyList.UpdateRoleForUser:
                UserRoleUpdate des5 = JsonSerializer.Deserialize<UserRoleUpdate>(cr.Message.Value);
                usersController.UpdateRoleForUser(des5);
                break;
              case TopicKeyList.UpdateProfile:
                ProfileInput des6 = JsonSerializer.Deserialize<ProfileInput>(cr.Message.Value);
                usersController.UpdateProfile(des6);
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

    public void TwittorTopicListener()
    {
      var server = _iconfiguration.GetSection("ConsumerConfig:BootstrapServers").Value;
      var groupId = _iconfiguration.GetSection("ConsumerConfig:GroupId").Value;
      var config = new ConsumerConfig
      {
        BootstrapServers = server,
        GroupId = groupId,
        AutoOffsetReset = AutoOffsetReset.Earliest
      };
      var topic = TopicList.TwittorTopic;
      CancellationTokenSource cts = new CancellationTokenSource();
      Console.CancelKeyPress += (_, e) =>
      {
        e.Cancel = true; // prevent the process from terminating.
        cts.Cancel();
      };
      using (var consumer = new ConsumerBuilder<string, string>(config).Build())
      {
        LoggingConsole.Log("Connected to " + topic);
        consumer.Subscribe(topic);
        try
        {
          while (true)
          {
            var cr = consumer.Consume(cts.Token);
            Console.WriteLine($"Consumed record with key: {cr.Message.Key} and value: {cr.Message.Value}");
            //tambahin switch case utk setiap event key yang berbeda
            TwittorsController twittorController = new TwittorsController(_iconfiguration);
            switch (cr.Message.Key)
            {
              case TopicKeyList.AddTwot:
                TwotInput des1 = JsonSerializer.Deserialize<TwotInput>(cr.Message.Value);
                twittorController.AddTwot(des1);
                break;
              case TopicKeyList.DeleteTwot:
                int des2 = JsonSerializer.Deserialize<int>(cr.Message.Value);
                twittorController.DeleteTwot(des2);
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

    public void CommentTopicListener()
    {
      var server = _iconfiguration.GetSection("ConsumerConfig:BootstrapServers").Value;
      var groupId = _iconfiguration.GetSection("ConsumerConfig:GroupId").Value;
      var config = new ConsumerConfig
      {
        BootstrapServers = server,
        GroupId = groupId,
        AutoOffsetReset = AutoOffsetReset.Earliest
      };
      var topic = TopicList.CommentTopic;
      CancellationTokenSource cts = new CancellationTokenSource();
      Console.CancelKeyPress += (_, e) =>
      {
        e.Cancel = true; // prevent the process from terminating.
        cts.Cancel();
      };
      using (var consumer = new ConsumerBuilder<string, string>(config).Build())
      {
        LoggingConsole.Log("Connected to " + topic);
        consumer.Subscribe(topic);
        try
        {
          while (true)
          {
            var cr = consumer.Consume(cts.Token);
            Console.WriteLine($"Consumed record with key: {cr.Message.Key} and value: {cr.Message.Value}");
            //tambahin switch case utk setiap event key yang berbeda
            CommentsController commentsController = new CommentsController(_iconfiguration);
            switch (cr.Message.Key)
            {
              case TopicKeyList.AddComment:
                CommentInput des1 = JsonSerializer.Deserialize<CommentInput>(cr.Message.Value);
                commentsController.AddComment(des1);
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