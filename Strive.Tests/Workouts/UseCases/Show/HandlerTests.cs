using Microsoft.Extensions.Logging;
using Moq;
using Strive.Application.Workouts.UseCases.Show;
using Strive.Application.Workouts.UseCases.Show.Contracts;
using Strive.Core.Entities;

namespace Strive.Tests.Workouts.UseCases.Show;

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
    public async Task ShouldReturnTrueWhenUserHasWorkouts()
    {
        var workouts = new ResponseData[]
        {
            new ResponseData { WorkoutId = 1, WorkoutName = "Super treino de pernas" },
            new ResponseData { WorkoutId = 2, WorkoutName = "Super treino de costas" }
        };
        var request = new Request(1);
        _repositoryMock.Setup(r => r.GetAllWorkoutsAsync(request.UserId,
            It.IsAny<CancellationToken>())).ReturnsAsync(workouts);

        var result = await _handler.HandleAsync(request, CancellationToken.None);

        Assert.True(result.Success);
        Assert.Equal(200, result.StatusCode);
    }
}