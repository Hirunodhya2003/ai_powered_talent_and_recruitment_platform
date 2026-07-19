using AutoMapper;
using Application.DTOs.Users;
using Domain.Entities;
using Application.DTOs.Jobs;
//using Domain.Entities;
using Application.DTOs.JobApplications;
using Application.DTOs.JobRequisitions;
using Application.DTOs.Interviews;
using Application.DTOs.Notifications;
using Application.DTOs.InterviewFeedbacks;
using Application.DTOs.HiringDecisions;

namespace Application.Mappings;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>();

        CreateMap<CreateUserDto, User>();

        CreateMap<UpdateUserDto, User>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());

        CreateMap<User, UpdateUserDto>();

        CreateMap<JobApplication, JobApplicationResponseDto>();

CreateMap<CreateJobApplicationDto, JobApplication>();

CreateMap<UpdateJobApplicationDto, JobApplication>();

CreateMap<Interview, InterviewResponseDto>();

CreateMap<CreateInterviewDto, Interview>();

CreateMap<UpdateInterviewDto, Interview>();

CreateMap<Notification, NotificationResponseDto>();

CreateMap<CreateNotificationDto, Notification>();

CreateMap<UpdateNotificationDto, Notification>();

CreateMap<Notification, NotificationResponseDto>();

CreateMap<CreateNotificationDto, Notification>();

CreateMap<UpdateNotificationDto, Notification>();

CreateMap<InterviewFeedback, InterviewFeedbackResponseDto>();

CreateMap<CreateInterviewFeedbackDto, InterviewFeedback>();

CreateMap<UpdateInterviewFeedbackDto, InterviewFeedback>();

CreateMap<CreateHiringDecisionDto, HiringDecision>();

CreateMap<UpdateHiringDecisionDto, HiringDecision>();

CreateMap<HiringDecision, HiringDecisionResponseDto>();

CreateMap<JobRequisition, JobRequisitionResponseDto>()
    .ForMember(
        dest => dest.ApprovalStatus,
        opt => opt.MapFrom(src => src.ApprovalStatus.ToString()));

        CreateMap<Job, JobResponseDto>()
    .ForMember(
        dest => dest.Status,
        opt => opt.MapFrom(src => src.Status.ToString()));
    }
}