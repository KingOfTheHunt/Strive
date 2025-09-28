using Strive.Core.Abstractions;
using Strive.Core.Enums;

namespace Strive.Core.Entities;

public class Exercise : Entity
{
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public EExerciseCategory Category { get; private set; }
    public IList<WorkoutExercise> WorkoutExercises { get; private set; } = [];
}