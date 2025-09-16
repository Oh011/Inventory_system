using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using InventorySystem.Application.Features.Notifications.Dtos;

namespace InventorySystem.Application.Features.Notifications.Mapping
{
    public class NotificationsMappingProfile : Profile
    {



        public NotificationsMappingProfile()
        {

            CreateMap<Notification, UserNotificationDto>()
                .ForMember(dest => dest.NotificationType,
                    opt => opt.MapFrom(src => src.Type.ToString())).ForMember(


                dest => dest.NotificationId, opt => opt.MapFrom(src => src.Id)) // enum => string
                .ReverseMap()
                .ForMember(dest => dest.Type,
                    opt => opt.MapFrom(src => Enum.Parse<NotificationType>(src.NotificationType)));


        }
    }
}
