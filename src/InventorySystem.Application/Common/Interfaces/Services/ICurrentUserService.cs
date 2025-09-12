namespace Project.Application.Common.Interfaces.Services
{
    public interface ICurrentUserService
    {

        string UserId { get; }
        string Role { get; }
        string? Email { get; }
    }
}
