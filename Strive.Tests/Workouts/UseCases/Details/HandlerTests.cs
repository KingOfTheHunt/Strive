using Microsoft.Extensions.Logging;
using Moq;
using Strive.Application.Workouts.UseCases.Details;
using Strive.Application.Workouts.UseCases.Details.Contracts;

namespace Strive.Tests.Workouts.UseCases.Details;

public class HandlerTests
{
    private readonly Mock<IRepository> _repositoryMock;
    private readonly Mock<ILogger<Handler>> _loggerMock;
    private readonly Handler _handler;

    public HandlerTests()
    {
        _repositoryMock = new();
        _loggerMock = new();
        _handler = new(_repositoryMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task ShouldReturnTrueWhenWorkoutHasExercises()
    {
        var data = new List<ResponseData>()
        {
            new ResponseData
            {
                ExerciseName = "Exercício 1", Sets = 3, Repetitions = 10, Weight = null,
                Duration = null
            },
            new ResponseData
            {
                ExerciseName = "Exercício 2", Sets = 3, Repetitions = 10, Weight = null,
                Duration = null
            }
        };
        var request = new Request(1, 1);
        _repositoryMock.Setup(r => r.AnyWorkoutAsync(request.WorkoutId, request.UserId,
            It.IsAny<CancellationToken>())).ReturnsAsync(true);
        _repositoryMock.Setup(r => r.GetExercisesAsync(request.WorkoutId, request.UserId,
            It.IsAny<CancellationToken>())).ReturnsAsync(data);

        var result = await _handler.HandleAsync(request);
        
        Assert.True(result.Success);
        Assert.Equal(200, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturnTrueWhenWorkoutExistAndHasNoExercises()
    {
        var request = new Request(1, 1);
        _repositoryMock.Setup(r => r.AnyWorkoutAsync(request.WorkoutId, request.UserId,
            It.IsAny<CancellationToken>())).ReturnsAsync(true);
        _repositoryMock.Setup(r => r.GetExercisesAsync(request.WorkoutId, request.UserId,
            It.IsAny<CancellationToken>())).ReturnsAsync([]);

        var result = await _handler.HandleAsync(request);
        
        Assert.True(result.Success);
        Assert.Equal(200, result.StatusCode);
        Assert.Empty(result.Data!);
    }

    [Fact]
    public async Task ReturnFalseWhenWorkoutDoesNotExist()
    {
        var request = new Request(1, 1);
        _repositoryMock.Setup(r => r.AnyWorkoutAsync(request.WorkoutId, request.UserId,
            It.IsAny<CancellationToken>())).ReturnsAsync(false);

        var result = await _handler.HandleAsync(request, CancellationToken.None);
        
        Assert.False(result.Success);
        Assert.Equal(404, result.StatusCode);
    }
}