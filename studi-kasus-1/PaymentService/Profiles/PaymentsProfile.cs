using System;
using AutoMapper;
using PaymentService.Dtos;
using PaymentService.Models;

namespace PaymentService.Profiles
{
  public class PaymentsProfile : Profile
  {
    public PaymentsProfile()
    {
      CreateMap<PaymentInput, Payment>();
      CreateMap<Payment, PaymentOutput>()
      .ForMember(dest => dest.Bill,
          opt => opt.MapFrom(src => Convert.ToDecimal(src.PaymentId * 10000f)));
      ;
    }
  }
}
