using MediatR;
using Project.Application.Features.Dashboard.Dtos;

namespace Project.Application.Features.Dashboard.Queries
{
    public record GetAdminDashboardQuery : IRequest<DashboardDto>;

}
