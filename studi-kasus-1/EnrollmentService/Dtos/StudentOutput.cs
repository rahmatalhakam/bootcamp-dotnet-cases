using System;
using System.Collections.Generic;
using EnrollmentService.Models;

namespace EnrollmentService.Dtos
{
  public class StudentOutput
  {
    public int ID { get; set; }
    public string Name { get; set; }
    public DateTime EnrollmentDate { get; set; }
    public ICollection<Enrollment> Enrollment { get; set; }
  }
}
