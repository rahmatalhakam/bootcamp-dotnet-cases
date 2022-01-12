using System;
using AutoMapper;

namespace EnrollmentService.Profiles
{
  public class CourseProfiles : Profile
  {
    public CourseProfiles()
    {
      CreateMap<Models.Course, Dtos.CourseOutput>()
          .ForMember(dest => dest.TotalHours,
          opt => opt.MapFrom(src => Convert.ToDecimal(src.Credits) * 1.5m));
      CreateMap<Dtos.CourseInput, Models.Course>();
    }
  }
}
