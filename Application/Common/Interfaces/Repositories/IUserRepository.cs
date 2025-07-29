namespace Project.Application.Common.Interfaces.Repositories
{
    public interface IUserRepository
    {


        Task<List<string>> GetUsersIdsInRole(List<string> roles);
    }
}
