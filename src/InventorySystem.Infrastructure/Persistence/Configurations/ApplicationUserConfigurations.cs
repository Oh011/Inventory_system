
using Domain.Entities;
using Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    internal class ApplicationUserConfigurations : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {



            builder.HasOne(u => u.employee).WithOne().HasForeignKey<Employee>(e => e.ApplicationUserId);

            builder.HasMany(u => u.Notifications).WithOne().HasForeignKey(n => n.UserId);

            builder.HasMany(u => u.RefreshTokens).WithOne(r => r.User)
                .HasForeignKey(r => r.UserId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
