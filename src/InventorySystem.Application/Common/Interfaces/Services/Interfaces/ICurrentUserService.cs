namespace InventorySystem.Application.Common.Interfaces.Services.Interfaces
{
    public interface ICurrentUserService
    {

        string UserId { get; }
        string Role { get; }
        string? Email { get; }
    }
}
