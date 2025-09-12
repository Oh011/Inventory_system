
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    internal class NotificationsConfigurations : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {


            builder.ToTable("Notifications");

            builder.HasKey(n => n.Id);

            builder.Property(n => n.Message)
                .IsRequired()
                .HasMaxLength(500); // Adjust as needed

            builder.Property(n => n.CreatedAt)
                .IsRequired();


            builder.Property(n => n.Type).HasConversion(n => n.ToString(),
                n => Enum.Parse<NotificationType>(n));

            // One-to-many with UserNotifications
            builder.HasMany(n => n.UserNotifications)
                .WithOne(un => un.Notification)
                .HasForeignKey(un => un.NotificationId)
                .OnDelete(DeleteBehavior.Cascade); // Delete user notifications when notification is deleted
        }
    }
}
