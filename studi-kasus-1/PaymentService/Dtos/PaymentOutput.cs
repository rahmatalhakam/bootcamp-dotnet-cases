using System;
using System.ComponentModel.DataAnnotations;

namespace PaymentService.Dtos
{
  public class PaymentOutput
  {

    public int PaymentId { get; set; }
    public int EnrollmentId { get; set; }
    public int CourseId { get; set; }
    public int StudentId { get; set; }
    public float Bill { get; set; }
  }
}
