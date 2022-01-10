using Microsoft.Extensions.Configuration;
using TwittorDAL.Data;
using TwittorDAL.Dtos;
using TwittorDAL.Helpers;

namespace TwittorDAL.Controllers
{
  public class CommentsController
  {
    private IConfiguration _iconfiguration;

    public CommentsController(IConfiguration iconfiguration)
    {
      _iconfiguration = iconfiguration;
    }

    public void AddComment(CommentInput input)
    {
      CommentDAL comment = new CommentDAL(_iconfiguration);
      var result = comment.AddComment(input);
      if (result.Result) LoggingConsole.Log("Adding comment succeded");
      else LoggingConsole.Log("Adding comment failed");
    }
  }
}