using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Strive.Core.Entities;

namespace Strive.Infrastructure.Mappings;

public class ExerciseMap : IEntityTypeConfiguration<Exercise>
{
    public void Configure(EntityTypeBuilder<Exercise> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasColumnName("Name")
            .HasColumnType("NVARCHAR(50)");

        builder.Property(x => x.Description)
            .IsRequired()
            .HasColumnName("Description")
            .HasColumnType("NVARCHAR(200)");

        builder.Property(x => x.Category)
            .IsRequired()
            .HasConversion<string>()
            .HasColumnName("Category")
            .HasColumnType("VARCHAR(15)");

        builder.HasIndex(x => x.Name, "IX_Exercises_Name");

        builder.Ignore(x => x.Notifications);
    }
}