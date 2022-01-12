using System;
using System.ComponentModel.DataAnnotations;
using EnrollmentService.ValidationAttributes;

namespace EnrollmentService.Dtos
{
  [StudentFirstLastMustBeDifferent]
  public class StudentInput
  {
    [Required(ErrorMessage = "Kolom FirstName harus diisi.")]
    [MaxLength(20)]
    public string FirstName { get; set; }
    [Required(ErrorMessage = "Kolom LatsName harus diisi.")]
    [MaxLength(20, ErrorMessage = "Tidak boleh lebih dari 20 karakter.")]
    public string LastName { get; set; }
    [Required]
    public DateTime EnrollmentDate { get; set; }

  }
}
