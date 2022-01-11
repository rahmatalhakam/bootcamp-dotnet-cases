using System;

namespace TwittorLog.Helpers
{
  public class LoggingConsole
  {
    public static void Log(string info)
    {
      Console.WriteLine("Logging ==> Date: " + DateTime.Now + " Info: " + info);
    }
  }
}