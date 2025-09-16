
using AutoMapper;
using Domain.Entities;
using InventorySystem.Application.Common.Interfaces.Services.Interfaces;
using InventorySystem.Application.Common.Interfaces.Repositories;
using InventorySystem.Application.Features.Notifications.Dtos;
using InventorySystem.Application.Features.Notifications.Interfaces;
using InventorySystem.Application.Features.Notifications.Queries.GetUserNotifications;
using InventorySystem.Application.Features.Notifications.Specifications;
using Shared.Results;


namespace InventorySystem.Services
{
    internal class UserNotificationsService(IUnitOfWork unitOfWork, IMapper mapper, IUriService uriService) : IUserNotificationsService
    {


        public async Task<UserNotificationDto> CreateNotification(CreateUserNotificationDto dto)
        {


            var notificationRepository = unitOfWork.GetRepository<Notification, int>();

            var notification = new Notification
            {
                Message = dto.Message,
                CreatedAt = DateTime.Now,
                Type = dto.NotificationType,
                UserNotifications = dto.Users.Select(id => new UserNotification
                {
                    UserId = id,
                    IsSeen = false

                }).ToList(),


            };


            await notificationRepository.AddAsync(notification);
            await unitOfWork.Commit();



            return new UserNotificationDto
            {
                NotificationId = notification.Id,
                Message = notification.Message,
                NotificationType = notification.Type.ToString(),

                IsSeen = false

            };

        }


        public async Task<IEnumerable<UserNotificationDto>> CreateNotifications(List<CreateUserNotificationDto> notifications)
        {


            var notificationRepository = unitOfWork.GetRepository<Notification, int>();


            List<Notification> createdNotifications = new List<Notification>();


            foreach (var item in notifications)
            {


                createdNotifications.Add(


            new Notification
            {
                Message = item.Message,
                CreatedAt = DateTime.Now,
                Type = item.NotificationType,
                UserNotifications = item.Users.Select(id => new UserNotification
                {
                    UserId = id,
                    IsSeen = false

                }).ToList(),


            }

                    );
            }



            await notificationRepository.AddRangeAsync(createdNotifications);
            await unitOfWork.Commit();



            return mapper.Map<List<UserNotificationDto>>(createdNotifications);



        }

        public async Task<PaginatedResult<UserNotificationDto>> GetUserNotifications(GetUserNotificationsQuery query)
        {

            var repository = unitOfWork.GetRepository<Notification, int>();


            var specifications = new UserNotificationsSpecification(query);


            var result = await repository.GetAllWithProjectionSpecifications(specifications);

            var totalCount = await repository.CountAsync(specifications.Criteria);


            return new PaginatedResult<UserNotificationDto>(

                query.PageIndex, query.pageSize, totalCount, result
                );

        }
    }
}
