using System;
using System.ComponentModel.DataAnnotations;

namespace EnrollmentService.Models
{
  public enum Grade
  {
    A, B, C, D, E, F
  }
  public class Enrollment
  {

    [Key]
    public int EnrollmentId { get; set; }
    public int CourseId { get; set; }
    public int StudentId { get; set; }
    public Grade? Grade { get; set; }

    public Course Course { get; set; }
    public Student Student { get; set; }
  }
}
