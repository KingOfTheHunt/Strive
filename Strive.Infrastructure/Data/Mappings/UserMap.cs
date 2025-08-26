using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Strive.Core.Entities;

namespace Strive.Infrastructure.Data.Mappings;

public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .IsRequired()
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();

        builder.OwnsOne(x => x.Name)
            .Property(y => y.FirstName)
            .IsRequired()
            .HasColumnName("FirstName")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(20);

        builder.OwnsOne(x => x.Name)
            .Property(y => y.LastName)
            .IsRequired()
            .HasColumnName("LastName")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(30);

        builder.OwnsOne(x => x.Email)
            .Property(y => y.Address)
            .IsRequired()
            .HasColumnName("Email")
            .HasColumnType("VARCHAR")
            .HasMaxLength(100);

        builder.OwnsOne(x => x.Email)
            .OwnsOne(y => y.Verification)
            .Property(z => z.Code)
            .IsRequired()
            .HasColumnName("VerificationCode")
            .HasColumnType("CHAR")
            .HasMaxLength(6);

        builder.OwnsOne(x => x.Email)
            .OwnsOne(y => y.Verification)
            .Property(z => z.ExpiresAt)
            .IsRequired(false)
            .HasColumnName("VerificationExpiresAt")
            .HasColumnType("DATETIME");

        builder.OwnsOne(x => x.Email)
            .OwnsOne(y => y.Verification)
            .Property(z => z.VerifiedAt)
            .IsRequired(false)
            .HasColumnName("VerificationVerifiedAt")
            .HasColumnType("DATETIME");

        builder.OwnsOne(x => x.Password)
            .Property(y => y.Hash)
            .IsRequired()
            .HasColumnName("PasswordHash")
            .HasColumnType("VARCHAR")
            .HasMaxLength(100);

        builder.OwnsOne(x => x.Password)
            .Property(y => y.ResetCode)
            .IsRequired()
            .HasColumnName("PasswordResetCode")
            .HasColumnType("CHAR")
            .HasMaxLength(6);

        builder.OwnsOne(x => x.Email)
            .HasIndex(y => y.Address)
            .IsUnique();
        
        builder.Ignore(x => x.Notifications);
        builder.OwnsOne(x => x.Name)
            .Ignore(y => y.Notifications);
        builder.OwnsOne(x => x.Email)
            .Ignore(y => y.Notifications);
        builder.OwnsOne(x => x.Email)
            .OwnsOne(y => y.Verification)
            .Ignore(z => z.Notifications);
        builder.OwnsOne(x => x.Password)
            .Ignore(y => y.Notifications);
    }
}