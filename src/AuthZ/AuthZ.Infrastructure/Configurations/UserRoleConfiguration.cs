using System.Runtime.InteropServices.Marshalling;
using AuthZ.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthZ.Infrastructure.Configurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.ToTable("UserRoles");

        builder.HasKey(ur => new{ ur.UserId, ur.RoleId});

        builder.Property(ur => ur.CreatedAt).IsRequired();
    }
}