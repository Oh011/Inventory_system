using MediatR;
using Project.Application.Features.Users.Dtos;
using Shared.Parameters;
using Shared.Results;

namespace Project.Application.Features.Users.Queries.GetAllUsers
{


    public class GetUsersQuery : PaginationQueryParameters, IRequest<PaginatedResult<UserSummaryDto>>
    {
        public string? email { get; set; }


        public string? UserName { get; set; }
        public string? Role { get; set; }

    }

}
