using System.Text.Json;
using System.Threading.Tasks;
using GraphQLAuth.Helper;
using HotChocolate;
using Microsoft.Extensions.Options;
using Twittor.Constants;
using Twittor.Data;
using Twittor.Dtos;
using Twittor.KafkaHandlers;
using Twittor.Models;

namespace Twittor.GraphQL
{
  public class Mutation
  {
    private readonly KafkaConfig _config;

    /** TODO:
5. login -> langsung ambil database
*/

    public Mutation([Service] IOptions<KafkaConfig> config)
    {
      _config = config.Value;
    }

    // public UserOutput UserLogin(LoginInput login, [Service] AppDbContext context, [Service] IOptions<AppSettings> _appSettings)
    // {
    //   string loginHash = ComputeHash.ComputeSha256HashFunc(login.Password);
    //   var result = context.Users.Where(co => co.Username == login.Username && co.Password == loginHash).SingleOrDefault();
    //   if (result == null)
    //     throw new UserNotFoundException();
    //   // List<Claim> claims = new List<Claim>();
    //   // claims.Add(new Claim(ClaimTypes.Name, result.Username));
    //   // var roles = await GetRolesFromUser(username);
    //   // foreach (var role in roles)
    //   // {
    //   //   claims.Add(new Claim(ClaimTypes.Role, role));
    //   // }
    //   var userToken = new UserToken
    //   {
    //     Email = result.Email,
    //     FullName = result.FullName,
    //     Id = result.Id,
    //     Username = result.Username
    //   };

    //   var tokenHandler = new JwtSecurityTokenHandler();
    //   var key = Encoding.ASCII.GetBytes(_appSettings.Value.Secret);
    //   var tokenDescriptor = new SecurityTokenDescriptor
    //   {
    //     // Subject = new ClaimsIdentity(claims),
    //     Expires = DateTime.UtcNow.AddHours(1),
    //     SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
    //       SecurityAlgorithms.HmacSha256Signature)
    //   };

    //   var token = tokenHandler.CreateToken(tokenDescriptor);
    //   userToken.Token = tokenHandler.WriteToken(token);
    //   return userToken;
    // }
    public async Task<string> Register(RegisterInput register)
    {
      try
      {
        var content = JsonSerializer.Serialize(register);
        var result = await Producerhandler.ProduceMessage(TopicList.UserTopic, _config, TopicKeyList.Registration, content);
        if (result) return "Produce register message succeed.";
        else throw new System.Exception("Produce register message failed.");
      }
      catch (System.Exception ex)
      {
        return ex.Message;
      }
    }

    public async Task<string> UpdatePassword(UpdatePassInput input)
    {
      try
      {
        var content = JsonSerializer.Serialize(input);
        var result1 = await Producerhandler.ProduceMessage(TopicList.UserTopic, _config, TopicKeyList.UpdatePassword, content);
        var result2 = await Producerhandler.ProduceMessage(TopicList.LoggingTopic, _config, TopicKeyList.UpdatePassword, content);
        if (result1 && result2) return "Produce update password message succeed.";
        else throw new System.Exception("Produce update password message failed.");
      }
      catch (System.Exception ex)
      {
        return ex.Message;
      }
    }

    public async Task<string> LockUser(LockUserInput input)
    {
      try
      {
        var content = JsonSerializer.Serialize(input);
        var result1 = await Producerhandler.ProduceMessage(TopicList.UserTopic, _config, TopicKeyList.LockUser, content);
        var result2 = await Producerhandler.ProduceMessage(TopicList.LoggingTopic, _config, TopicKeyList.LockUser, content);
        if (result1 && result2) return "Produce lock user message succeed.";
        else throw new System.Exception("Produce lock user message failed.");
      }
      catch (System.Exception ex)
      {
        return ex.Message;
      }
    }

    public async Task<string> AddRoleForUser(UserRoleInput input)
    {
      try
      {
        var content = JsonSerializer.Serialize(input);
        var result1 = await Producerhandler.ProduceMessage(TopicList.UserTopic, _config, TopicKeyList.AddRoleForUser, content);
        var result2 = await Producerhandler.ProduceMessage(TopicList.LoggingTopic, _config, TopicKeyList.AddRoleForUser, content);
        if (result1 && result2) return "Produce add role for user message succeed.";
        else throw new System.Exception("Produce add role for user message failed.");
      }
      catch (System.Exception ex)
      {
        return ex.Message;
      }
    }

    public async Task<string> UpdateRoleForUser(UserRoleUpdate input)
    {
      try
      {
        var content = JsonSerializer.Serialize(input);
        var result1 = await Producerhandler.ProduceMessage(TopicList.UserTopic, _config, TopicKeyList.UpdateRoleForUser, content);
        var result2 = await Producerhandler.ProduceMessage(TopicList.LoggingTopic, _config, TopicKeyList.UpdateRoleForUser, content);
        if (result1 && result2) return "Produce update role for user message succeed.";
        else throw new System.Exception("Produce update role for user message failed.");
      }
      catch (System.Exception ex)
      {
        return ex.Message;
      }
    }

    public async Task<string> UpdateProfile(ProfileInput input)
    {
      try
      {
        var content = JsonSerializer.Serialize(input);
        var result1 = await Producerhandler.ProduceMessage(TopicList.UserTopic, _config, TopicKeyList.UpdateProfile, content);
        var result2 = await Producerhandler.ProduceMessage(TopicList.LoggingTopic, _config, TopicKeyList.UpdateProfile, content);
        if (result1 && result2) return "Produce update profile message succeed.";
        else throw new System.Exception("Produce update profile message failed.");
      }
      catch (System.Exception ex)
      {
        return ex.Message;
      }
    }

    public async Task<string> AddTwot(TwotInput input)
    {
      try
      {
        var content = JsonSerializer.Serialize(input);
        var result1 = await Producerhandler.ProduceMessage(TopicList.TwittorTopic, _config, TopicKeyList.AddTwot, content);
        var result2 = await Producerhandler.ProduceMessage(TopicList.LoggingTopic, _config, TopicKeyList.AddTwot, content);
        if (result1 && result2) return "Produce add twot message succeed.";
        else throw new System.Exception("Produce add twot message failed.");
      }
      catch (System.Exception ex)
      {
        return ex.Message;
      }
    }

    public async Task<string> DeleteTwot(int input)
    {
      try
      {
        var content = JsonSerializer.Serialize(input);
        var result1 = await Producerhandler.ProduceMessage(TopicList.TwittorTopic, _config, TopicKeyList.DeleteTwot, content);
        var result2 = await Producerhandler.ProduceMessage(TopicList.LoggingTopic, _config, TopicKeyList.DeleteTwot, content);
        if (result1 && result2) return "Produce delete twot message succeed.";
        else throw new System.Exception("Produce delete twot message failed.");
      }
      catch (System.Exception ex)
      {
        return ex.Message;
      }
    }

    public async Task<string> AddComment(CommentInput input)
    {
      try
      {
        var content = JsonSerializer.Serialize(input);
        var result1 = await Producerhandler.ProduceMessage(TopicList.CommentTopic, _config, TopicKeyList.AddComment, content);
        var result2 = await Producerhandler.ProduceMessage(TopicList.LoggingTopic, _config, TopicKeyList.AddComment, content);
        if (result1 && result2) return "Produce add comment message succeed.";
        else throw new System.Exception("Produce add comment message failed.");
      }
      catch (System.Exception ex)
      {
        return ex.Message;
      }
    }



  }
}