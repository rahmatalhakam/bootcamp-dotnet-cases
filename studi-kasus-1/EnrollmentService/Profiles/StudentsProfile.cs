using System;
using AutoMapper;

namespace EnrollmentService.Profiles
{
  public class StudentsProfile : Profile
  {
    public StudentsProfile()
    {
      CreateMap<Models.Student, Dtos.StudentOutput>()
          .ForMember(dest => dest.Name,
          opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));
      CreateMap<Dtos.StudentInput, Models.Student>();
    }
  }
}
