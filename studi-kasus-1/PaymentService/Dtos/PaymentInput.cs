using System;
using System.ComponentModel.DataAnnotations;

namespace PaymentService.Dtos
{
  public class PaymentInput
  {

    public int EnrollmentId { get; set; }
    public int CourseId { get; set; }
    public int StudentId { get; set; }
  }
}
