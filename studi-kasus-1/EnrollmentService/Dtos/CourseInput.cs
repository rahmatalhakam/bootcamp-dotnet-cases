using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EnrollmentService.Dtos
{
  public class CourseInput : IValidatableObject
  {
    [Required]
    public string Title { get; set; }

    [Required]
    public int Credits { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
      if (Title.Length > 50)
        yield return new ValidationResult("Title maksimal 50 karakter.",
            new[] { "Title" });
      if (!Title.StartsWith("Training"))
        yield return new ValidationResult("Title harus dimulai dengan kata 'Training'",
            new[] { "Title" });
      if (Credits > 10)
        yield return new ValidationResult("Credits maksimal 10'",
            new[] { "Credits" });
    }
  }
}
