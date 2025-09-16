namespace InventorySystem.Application.Features.Users.Dtos
{
    public class CreateUserDto
    {

        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }


        public string FullName { get; set; }
        public string Role { get; set; } // or enum
    }
}
