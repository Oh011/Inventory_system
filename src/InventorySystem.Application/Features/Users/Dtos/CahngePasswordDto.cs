namespace InventorySystem.Application.Features.Users.Dtos
{
    public class ChangePasswordDto
    {

        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
