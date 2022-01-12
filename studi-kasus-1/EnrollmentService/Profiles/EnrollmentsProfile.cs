using System;
using AutoMapper;
using EnrollmentService.Dtos;
using EnrollmentService.Models;

namespace EnrollmentService.Profiles
{
  public class EnrollmentsProfile : Profile
  {
    public EnrollmentsProfile()
    {
      CreateMap<EnrollmentInput, Enrollment>();
      CreateMap<Enrollment, EnrollmentInput>();
    }
  }
}
