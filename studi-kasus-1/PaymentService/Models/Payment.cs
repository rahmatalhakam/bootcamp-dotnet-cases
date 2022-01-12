using System;
using System.ComponentModel.DataAnnotations;

namespace PaymentService.Models
{
  public class Payment
  {

    [Key]
    public int PaymentId { get; set; }
    public int EnrollmentId { get; set; }
    public int CourseId { get; set; }
    public int StudentId { get; set; }
  }
}
