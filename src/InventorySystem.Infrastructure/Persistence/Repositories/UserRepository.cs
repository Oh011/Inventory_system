using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Project.Application.Common.Interfaces.Repositories;
using System.Data;

namespace Infrastructure.Persistence.Repositories
{
    internal class UserRepository : IUserRepository
    {

        private readonly ApplicationDbContext _context;


        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<string>> GetUsersIdsInRole(List<string> roles)
        {

            var usersIds = await (from user in _context.Users
                                  join userRole in _context.UserRoles on user.Id equals userRole.UserId
                                  join role in _context.Roles on userRole.RoleId equals role.Id
                                  where roles.Contains(role.Name)
                                  select user.Id).Distinct().ToListAsync();


            return usersIds;
        }
    }
}
