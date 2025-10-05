using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Strive.Core.Entities;

namespace Strive.Infrastructure.Mappings;

public class WorkoutSessionMap : IEntityTypeConfiguration<WorkoutSession>
{
    public void Configure(EntityTypeBuilder<WorkoutSession> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();

        builder.OwnsOne(x => x.Commentary)
            .Property(y => y.Commentary)
            .IsRequired(false)
            .HasColumnName("Commentary")
            .HasColumnType("NVARCHAR(200)");

        builder.OwnsOne(x => x.ScheduledAt)
            .Property(y => y.Date)
            .IsRequired(false)
            .HasColumnName("ScheduledAt")
            .HasColumnType("DATETIME2");

        builder.Property(x => x.Done)
            .IsRequired()
            .HasColumnName("Done")
            .HasColumnType("BIT")
            .HasDefaultValue(false);

        builder.HasOne(x => x.Workout)
            .WithMany(y => y.WorkoutSessions)
            .HasForeignKey(x => x.WorkoutId)
            .HasConstraintName("FK_Workouts_WorkoutSessions")
            .OnDelete(DeleteBehavior.Cascade);

        builder.Ignore(x => x.Notifications);
        builder.OwnsOne(x => x.Commentary)
            .Ignore(y => y.Notifications);
        builder.OwnsOne(x => x.ScheduledAt)
            .Ignore(y => y.Notifications);
    }
}