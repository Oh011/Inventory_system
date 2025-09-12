namespace Project.Application.Features.Users.Dtos
{
    public class UserSummaryDto
    {
        public string Id { get; set; }           // Unique identifier
        public string FullName { get; set; }     // Display name
        public string UserName { get; set; }     // For login
        public string Email { get; set; }        // Contact info

        public string Role { get; set; }         // For role-based UI/actions
    }

}
