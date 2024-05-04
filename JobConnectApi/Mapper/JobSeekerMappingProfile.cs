using AutoMapper;
using JobConnectApi.DTOs;
using JobConnectApi.Models;

namespace JobConnectApi.Mapper;

public class JobSeekerMappingProfile:Profile
{
    public JobSeekerMappingProfile()
    {
        CreateMap<JobSeeker, JobSeekerDto>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
    }
}