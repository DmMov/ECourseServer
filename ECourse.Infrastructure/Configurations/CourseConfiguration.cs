using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ECourse.Domain.Entities;
using System;

namespace ECourse.Infrastructure.Persistence.Configurations
{
    class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.Property(x => x.Name)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasMaxLength(5000)
                .IsRequired();

            builder.Property(x => x.UserId)
                .IsRequired();
        }
    }
}
