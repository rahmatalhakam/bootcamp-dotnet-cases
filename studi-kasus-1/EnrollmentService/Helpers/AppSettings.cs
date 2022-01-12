using System;
namespace EnrollmentService.Helpers
{
  public class AppSettings
  {
    public string Secret { get; set; }
    public string AuthUrl { get; set; }
    public string PaymentUrl { get; set; }
  }
}
