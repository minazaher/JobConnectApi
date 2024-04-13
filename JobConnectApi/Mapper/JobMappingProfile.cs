using AutoMapper;
using JobConnectApi.DTOs;
using JobConnectApi.Models;

namespace JobConnectApi.Mapper;

public class JobMappingProfile : Profile
{
    public JobMappingProfile()
    {
        CreateMap<JobRequest, Job>()
            .ForMember(dest => dest.JobId, opt => opt.MapFrom(src => src.JobId)) // Explicit mapping for primitive types
            .ForMember(dest => dest.JobTitle, opt => opt.MapFrom(src => src.JobTitle))
            .ForMember(dest => dest.JobType, opt => opt.MapFrom(src => src.JobType))
            .ForMember(dest => dest.Salray, opt => opt.MapFrom(src => src.Salray))
            .ForMember(dest => dest.PostDate, opt => opt.MapFrom(src => src.PostDate))
            .ForMember(dest => dest.JobDescription, opt => opt.MapFrom(src => src.JobDescription))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
            .ForMember(dest => dest.Employer, opt => opt.Ignore())
            .ForMember(dest => dest.Admin, opt => opt.Ignore());
    }
}
