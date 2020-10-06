using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ECourse.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECourse.Infrastructure.Persistence.Configurations
{
    class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(64);

            builder.Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(64);

            builder.Property(x => x.Email)
                .IsRequired();

            builder.Property(x => x.DateOfBirth)
                .IsRequired();
        }
    }
}
