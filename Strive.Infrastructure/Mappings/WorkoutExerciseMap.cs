using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Strive.Core.Entities;

namespace Strive.Infrastructure.Mappings;

public class WorkoutExerciseMap : IEntityTypeConfiguration<WorkoutExercise>
{
    public void Configure(EntityTypeBuilder<WorkoutExercise> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();

        builder.OwnsOne(x => x.Sets)
            .Property(y => y.Sets)
            .IsRequired()
            .HasColumnName("Sets")
            .HasColumnType("TINYINT");

        builder.OwnsOne(x => x.Repetitions)
            .Property(y => y.Repetitions)
            .IsRequired(false)
            .HasColumnName("Repetitions")
            .HasColumnType("TINYINT");

        builder.OwnsOne(x => x.Weight)
            .Property(y => y.Weight)
            .HasColumnName("Weight")
            .HasColumnType("REAL");

        builder.OwnsOne(x => x.Duration)
            .Property(y => y.TimeInSeconds)
            .IsRequired(false)
            .HasColumnName("Duration")
            .HasColumnType("INT");

        builder.HasOne(x => x.Workout)
            .WithMany(y => y.WorkoutExercises)
            .HasForeignKey(x => x.WorkoutId)
            .HasConstraintName("FK_Workouts_WorkoutsExercises")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Exercise)
            .WithMany(y => y.WorkoutExercises)
            .HasForeignKey(x => x.ExerciseId)
            .HasConstraintName("FK_Exercises_WorkoutExercises")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new { x.WorkoutId, x.ExerciseId },
                "IX_WorkoutExercises_WorkoutId_ExerciseId")
            .IsUnique();

        builder.Ignore(x => x.Notifications);
        builder.OwnsOne(x => x.Sets)
            .Ignore(y => y.Notifications);
        builder.OwnsOne(x => x.Repetitions)
            .Ignore(y => y.Notifications);
        builder.OwnsOne(x => x.Weight)
            .Ignore(y => y.Notifications);
        builder.OwnsOne(x => x.Duration)
            .Ignore(y => y.Notifications);
    }
}