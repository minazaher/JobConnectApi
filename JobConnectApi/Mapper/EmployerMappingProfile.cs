using AutoMapper;
using JobConnectApi.DTOs;
using JobConnectApi.Models;

namespace JobConnectApi.Mapper;

public class EmployerMappingProfile:Profile
{
    public EmployerMappingProfile()
    {
        CreateMap<Employer, EmployerDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.CompanyName))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.Industry, opt => opt.MapFrom(src => src.Industry));
    }
}