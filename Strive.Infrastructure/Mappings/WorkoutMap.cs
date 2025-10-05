using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Strive.Core.Entities;

namespace Strive.Infrastructure.Mappings;

public class WorkoutMap : IEntityTypeConfiguration<Workout>
{
    public void Configure(EntityTypeBuilder<Workout> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasColumnName("Name")
            .HasColumnType("NVARCHAR(100)");

        builder.HasOne(x => x.User)
            .WithMany(y => y.Workouts)
            .HasForeignKey(x => x.UserId)
            .HasConstraintName("FK_Users_Workouts")
            .OnDelete(DeleteBehavior.Cascade);
    }
}