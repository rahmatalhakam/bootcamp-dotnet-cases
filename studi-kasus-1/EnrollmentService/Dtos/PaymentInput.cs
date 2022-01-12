using System;
using System.ComponentModel.DataAnnotations;

namespace EnrollmentService.Dtos
{
  public class PaymentInput
  {

    public int EnrollmentId { get; set; }
    public int CourseId { get; set; }
    public int StudentId { get; set; }
  }
}
