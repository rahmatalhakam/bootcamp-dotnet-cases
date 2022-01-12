using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnrollmentService.Models
{
  public class Course
  {
    [Key]
    public int CourseId { get; set; }

    [Required]
    [MaxLength(100)]
    public string Title { get; set; }

    [Required]
    public int Credits { get; set; }

    public ICollection<Enrollment> Enrollment { get; set; }
  }
}
