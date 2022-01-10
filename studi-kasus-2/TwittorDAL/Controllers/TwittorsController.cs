using Microsoft.Extensions.Configuration;
using TwittorDAL.Data;
using TwittorDAL.Dtos;
using TwittorDAL.Helpers;

namespace TwittorDAL.Controllers
{
  public class TwittorsController
  {
    private IConfiguration _iconfiguration;

    public TwittorsController(IConfiguration iconfiguration)
    {
      _iconfiguration = iconfiguration;
    }

    public void AddTwot(TwotInput input)
    {
      TwittorDALClass twot = new TwittorDALClass(_iconfiguration);
      var result = twot.AddTwot(input);
      if (result.Result) LoggingConsole.Log("Adding twot post succeded");
      else LoggingConsole.Log("Adding twot post failed");
    }

    public void DeleteTwot(int TwotID)
    {
      TwittorDALClass twot = new TwittorDALClass(_iconfiguration);
      var result = twot.DeleteTwot(TwotID);
      if (result.Result) LoggingConsole.Log("Deleting twot post succeded");
      else LoggingConsole.Log("Deleting twot post failed");
    }

  }
}