using System.Threading.Tasks;
using GraphQLAuth.Helper;
using HotChocolate;
using Twittor.Data;
using Twittor.Models;

namespace Twittor.GraphQL
{
  public class Mutation
  {
    /** TODO:
    1. posting twit
    2. posting comment
    3. delete tweet
    4. register
    5. login -> langsung ambil database
    6. edit profile
    7. change password
    8. lock user 
    9. change user role
    */
    // public async Task<UserPayload> Register(RegisterInput register, [Service] AppDbContext context)
    // {
    //   var user = new User
    //   {
    //     Email = register.Email,
    //     FullName = register.FullName,
    //     Password = ComputeHash.ComputeSha256HashFunc(register.Password),
    //     Username = register.Username

    //   };
    //   try
    //   {
    //     context.Users.Add(user);
    //     var result = await context.SaveChangesAsync();
    //     var UserPayload = new UserPayload
    //     {
    //       Email = register.Email,
    //       FullName = register.FullName,
    //       Username = register.Username
    //     };
    //     return UserPayload;
    //   }
    //   catch (System.Exception)
    //   {
    //     throw new DuplicateUsername();

    //   }

    // }

  }
}