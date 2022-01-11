using System;

namespace Twittor.Helpers
{
  public class LoggingConsole
  {
    public static void Log(string info)
    {
      Console.WriteLine("Logging ==> Date: " + DateTime.Now + " Info: " + info);
    }
  }
}