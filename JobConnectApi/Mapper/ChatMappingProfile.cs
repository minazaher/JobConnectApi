using AutoMapper;
using JobConnectApi.DTOs;
using JobConnectApi.Models;

namespace JobConnectApi.Mapper;

public class ChatMappingProfile:Profile
{
    public ChatMappingProfile()
    {
        CreateMap<Chat, ChatResponseDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Employer, opt => opt.MapFrom(src => src.Employer))
            .ForMember(dest => dest.JobSeeker, opt => opt.MapFrom(src => src.JobSeeker))
            .ForMember(dest => dest.JobSeekerId, opt => opt.MapFrom(src => src.JobSeekerId))
            .ForMember(dest => dest.EmployerId, opt => opt.MapFrom(src => src.EmployerId));
        
        CreateMap<JobSeeker, JobSeekerDto>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
    
    }
}