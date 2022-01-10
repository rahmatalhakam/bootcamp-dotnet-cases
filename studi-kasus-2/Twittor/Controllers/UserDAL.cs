using System;
using System.Text;
using System.Threading.Tasks;
using GraphQLAuth.Helper;
using Twittor.Data;
using Twittor.Dtos;
using Twittor.Models;

namespace Twittor.Controllers
{
  public class UserDAL
  {
    private readonly AppDbContext _context;

    public UserDAL(AppDbContext context)
    {
      _context = context;
    }
    // public async Task<UserOutput> Registration(RegisterInput userInput)
    // {
    //   try
    //   {
    //     var user = new User
    //     {
    //       Username = userInput.Username,
    //       Email = userInput.Email,
    //       Password = ComputeHash.ComputeSha256HashFunc(userInput.Password),
    //       FirstName = userInput.FirstName,
    //       LastName = userInput.LastName,
    //       CreatedAt = System.DateTime.Now,
    //       Lock = false
    //     };
    //     try
    //     {
    //       var result = await _context.Users.AddAsync(user);
    //       //   return user;
    //     }
    //     catch (Exception ex)
    //     {
    //       throw new Exception($"{ex.Message}");
    //     }
  }
}