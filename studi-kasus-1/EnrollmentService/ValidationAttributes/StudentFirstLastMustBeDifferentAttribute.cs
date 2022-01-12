
using System.ComponentModel.DataAnnotations;
using EnrollmentService.Dtos;

namespace EnrollmentService.ValidationAttributes
{
  public class StudentFirstLastMustBeDifferentAttribute : ValidationAttribute
  {

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
      var student = (StudentInput)validationContext.ObjectInstance;
      if (student.FirstName == student.LastName)
        return new ValidationResult("Firstname dan lastname tidak boleh sama",
            new[] { nameof(StudentInput) });
      return ValidationResult.Success;
    }

  }
}
