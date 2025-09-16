using MediatR;
using InventorySystem.Application.Features.Dashboard.Dtos;

namespace InventorySystem.Application.Features.Dashboard.Queries
{
    public record GetAdminDashboardQuery : IRequest<DashboardDto>;

}
