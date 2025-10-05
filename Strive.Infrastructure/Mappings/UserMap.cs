using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Strive.Core.Entities;

namespace Strive.Infrastructure.Mappings;

public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();

        builder.OwnsOne(x => x.Name, name =>
        {
            name.Property(x => x.FirstName)
                .IsRequired()
                .HasColumnName("FirstName")
                .HasColumnType("NVARCHAR(20)");

            name.Property(x => x.LastName)
                .IsRequired()
                .HasColumnName("LastName");

            name.Ignore(x => x.Notifications);
        });

        builder.OwnsOne(x => x.Email, email =>
        {
            email.Property(x => x.Address)
                .IsRequired()
                .HasColumnName("email")
                .HasColumnType("VARCHAR(100)");

            email.OwnsOne(x => x.Verification, verification =>
            {
                verification.Property(x => x.Code)
                    .IsRequired()
                    .HasColumnName("VerificationCode")
                    .HasColumnType("CHAR(6)");

                verification.Property(x => x.ExpiresAt)
                    .IsRequired(false)
                    .HasColumnName("VerificationCodeExpiresAt");

                verification.Property(x => x.VerifiedAt)
                    .IsRequired(false)
                    .HasColumnName("VerificationVerifiedAt");

                verification.Ignore(x => x.Notifications);
            });

            email.HasIndex(x => x.Address, "IX_Users_Email");
            
            email.Ignore(x => x.Notifications);
        });

        builder.OwnsOne(x => x.Password, password =>
        {
            password.Property(x => x.Hash)
                .IsRequired()
                .HasColumnName("PasswordHash")
                .HasColumnType("VARCHAR(100)");

            password.Property(x => x.ResetCode)
                .IsRequired()
                .HasColumnName("PasswordResetCode")
                .HasColumnType("CHAR(6)");

            password.Ignore(x => x.Notifications);
        });
        
        builder.Ignore(x => x.Notifications);
    }
}