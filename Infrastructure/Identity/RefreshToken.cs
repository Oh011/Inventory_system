namespace Infrastructure.Identity
{
    public class RefreshToken
    {


        public int Id { get; set; }
        public string UserId { get; set; }  // Link to ApplicationUser


        public string DeviceId { get; set; }  // New field to store DeviceId
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Revoked { get; set; }


        public ApplicationUser User { get; set; }  // Navigation property
    }

}
