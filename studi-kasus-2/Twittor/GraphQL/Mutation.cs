using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Twittor.Constants;
using Twittor.Data;
using Twittor.Dtos;
using Twittor.Helper;
using Twittor.Helpers;
using Twittor.KafkaHandlers;
using Twittor.Models;

namespace Twittor.GraphQL
{
  public class Mutation
  {
    private readonly KafkaConfig _config;
    private readonly AppSettings _appSettings;

    public Mutation([Service] IOptions<KafkaConfig> config, [Service] IOptions<AppSettings> appSettings)
    {
      _config = config.Value;
      _appSettings = appSettings.Value;
    }

    public async Task<UserOutput> UserLogin(LoginInput login, [Service] AppDbContext context)
    {
      string loginHash = ComputeHash.ComputeSha256HashFunc(login.Password);
      var result2 = context.Users.Where(co => co.Username == login.Username && co.Lock == false).SingleOrDefault();
      if (result2 == null)
        throw new UserLockedException();
      var result = context.Users.Where(co => co.Username == login.Username && co.Password == loginHash).Include(p => p.UserRoles).SingleOrDefault();
      if (result == null)
        throw new UserNotFoundException();
      List<Claim> claims = new List<Claim>();
      claims.Add(new Claim(ClaimTypes.Name, result.Username));

      foreach (var userRole in result.UserRoles)
      {
        var roleResult = context.Roles.Where(o => o.Id == userRole.RoleID).FirstOrDefault();
        if (roleResult != null)
        {
          claims.Add(new Claim(ClaimTypes.Role, roleResult.Name));
        }
      }
      var userToken = new UserOutput
      {
        Email = result.Email,
        CreatedAt = result.CreatedAt,
        Id = result.Id,
        Username = result.Username,
        FirstName = result.FirstName,
        LastName = result.LastName,
        Lock = result.Lock
      };

      var tokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(claims),
        Expires = DateTime.Now.AddHours(3),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
          SecurityAlgorithms.HmacSha256Signature)
      };

      var token = tokenHandler.CreateToken(tokenDescriptor);
      userToken.Token = tokenHandler.WriteToken(token);
      try
      {
        var content = JsonSerializer.Serialize(login);
        Boolean res = await Producerhandler.ProduceMessage(TopicList.LoggingTopic, _config, TopicKeyList.UserLogin, content);
        if (res) LoggingConsole.Log("Produce user message succeed.");
        else LoggingConsole.Log("Produce register message failed.");
      }
      catch (System.Exception)
      {
        throw;
      }
      return await Task.FromResult(userToken);
    }
    public async Task<string> Register(RegisterInput register)
    {
      try
      {
        var content = JsonSerializer.Serialize(register);
        var result1 = await Producerhandler.ProduceMessage(TopicList.UserTopic, _config, TopicKeyList.Registration, content);
        var result2 = await Producerhandler.ProduceMessage(TopicList.LoggingTopic, _config, TopicKeyList.Registration, content);
        if (result1 && result2) return "Produce register message succeed.";
        else throw new System.Exception("Produce register message failed.");
      }
      catch (System.Exception ex)
      {
        return ex.Message;
      }
    }

    [Authorize(Roles = new[] { "MEMBER", "ADMIN" })]
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

    [Authorize(Roles = new[] { "ADMIN" })]
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

    [Authorize(Roles = new[] { "ADMIN" })]
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

    [Authorize(Roles = new[] { "MEMBER", "ADMIN" })]
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

    [Authorize(Roles = new[] { "MEMBER" })]
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

    [Authorize(Roles = new[] { "MEMBER" })]
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

    [Authorize(Roles = new[] { "MEMBER" })]
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