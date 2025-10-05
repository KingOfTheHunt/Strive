using Flunt.Notifications;
using Microsoft.EntityFrameworkCore;
using Strive.Core.Entities;
using Strive.Core.Enums;

namespace Strive.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; private set; }
    public DbSet<Workout> Workouts { get; private set; }
    public DbSet<Exercise> Exercises { get; private set; }
    public DbSet<WorkoutExercise> WorkoutExercises { get; private set; }
    public DbSet<WorkoutSession> WorkoutSessions { get; private set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<Notification>();
        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DependencyInjection).Assembly);

        modelBuilder.Entity<Exercise>().HasData(
            new Exercise(1, "Running",
                "High-intensity activity that improves cardiovascular capacity and quickly burns calories.",
                EExerciseCategory.Cardio),
            new Exercise(2, "Jumping Jacks",
                "Aerobic exercise involving opening and closing arms and legs in sync, stimulating the heart and lungs.",
                EExerciseCategory.Cardio),
            new Exercise(3, "Squat",
                "Knee and hip flexion movement that strengthens legs and glutes.",
                EExerciseCategory.Strength),
            new Exercise(4, "Push-up", 
                "Exercise that uses body weight to strengthen chest, triceps, and shoulders.",
                EExerciseCategory.Strength),
            new Exercise(5, "Plank", 
                "Isometric position that activates abs, lower back, and shoulders, strengthening the core.",
                EExerciseCategory.Strength),
            new Exercise(6, "Lunge", 
                "Step forward with knee flexion, strengthening legs and glutes.",
                EExerciseCategory.Strength),
            new Exercise(7, "Hamstring Stretch", 
                "Bending the torso forward to stretch the back of the thighs.",
                EExerciseCategory.Flexibility),
            new Exercise(8, "Shoulder Stretch",
                "Pulling the arm across the chest to stretch deltoids and trapezius.",
                EExerciseCategory.Flexibility),
            new Exercise(9, "Cat-Cow Pose", 
                "Sequence of spinal flexion and extension movements that improve mobility and relaxation.",
                EExerciseCategory.Flexibility),
            new Exercise(10, "Squat Jack", 
                "A variation of jumping jacks combined with squats, working both cardiovascular endurance and leg strength.",
                EExerciseCategory.Cardio));
    }
}