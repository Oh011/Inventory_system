namespace InventorySystem.Application.Common.Interfaces
{
    public interface ILinkBuilder
    {
        string BuildPasswordResetLink(string email, string token);
    }

}
