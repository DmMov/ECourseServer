using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ECourse.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECourse.Infrastructure.Persistence.Configurations
{
    class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
    {
        public void Configure(EntityTypeBuilder<Subscription> builder)
        {
            builder.HasKey(x => new { x.CourseId, x.UserId });;

            builder.Property(x => x.UserId)
                .IsRequired();

            builder.Property(x => x.CourseId)
                .IsRequired();

            builder.HasOne(x => x.Course)
                .WithMany(x => x.Subscriptions)
                .HasForeignKey(x => x.CourseId);

            builder.HasOne(x => x.User)
                .WithMany(x => x.Subscriptions)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
