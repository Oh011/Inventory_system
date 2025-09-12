
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {


        public string FullName { get; set; }



        public Employee employee;



        public ICollection<UserNotification> Notifications { get; set; } = new List<UserNotification>();

        public ICollection<RefreshToken> RefreshTokens { get; set; }

    }
}
