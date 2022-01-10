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

  }
}