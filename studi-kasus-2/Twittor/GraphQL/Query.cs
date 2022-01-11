using System.Linq;
using System.Threading.Tasks;
using HotChocolate;
using Microsoft.Extensions.Options;
using Twittor.Constants;
using Twittor.Data;
using Twittor.Dtos;
using Twittor.Helper;
using Twittor.KafkaHandlers;
using Twittor.Models;

namespace Twittor.GraphQL
{
  public class Query
  {
    private KafkaConfig _config;
    public Query([Service] IOptions<KafkaConfig> config, [Service] IOptions<AppSettings> appSettings)
    {
      _config = config.Value;
    }
    public async Task<IQueryable<TwotOutput>> GetTwots([Service] AppDbContext context)
    {
      var result = await Producerhandler.ProduceMessage(TopicList.LoggingTopic, _config, TopicKeyList.GetTwot, "Get twots happened");

      return (context.TwittorModels.Select(p => new TwotOutput()
      {
        Id = p.Id,
        Description = p.Description,
        CreatedAt = p.CreatedAt,
        UserID = p.User.Id
      }));
    }


    public async Task<IQueryable<ProfileOutput>> GetProfiles(int userID, [Service] AppDbContext context)
    {
      var result = await Producerhandler.ProduceMessage(TopicList.LoggingTopic, _config, TopicKeyList.GetProfile, $"Get profile ID: {userID} happened");
      return context.Users.Where(u => u.Id == userID).Select(p => new ProfileOutput()
      {
        Id = p.Id,
        Email = p.Email,
        FirstName = p.FirstName,
        LastName = p.LastName,
        Username = p.Username
      });
    }


  }
}