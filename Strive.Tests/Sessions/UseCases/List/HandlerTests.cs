using Microsoft.Extensions.Logging;
using Moq;
using Strive.Application.Sessions.UseCases.List;
using Strive.Application.Sessions.UseCases.List.Contracts;

namespace Strive.Tests.Sessions.UseCases.List;

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
    public async Task ShouldReturnTrueWhenWorkoutHasSessions()
    {
        var data = new List<ResponseData>()
        {
            new ResponseData("Treino de pernas", DateTime.UtcNow.AddDays(-1), true),
            new ResponseData("Treino de costas", DateTime.UtcNow, false)
        };
        var request = new Request(1, DateTime.UtcNow.AddDays(-2), DateTime.UtcNow);
        _repositoryMock.Setup(r => r.GetScheduleWorkoutsByDate(request.UserId,
                request.StartDate, request.EndDate, It.IsAny<CancellationToken>()))
            .ReturnsAsync(data);

        var result = await _handler.HandleAsync(request, CancellationToken.None);
        
        Assert.True(result.Success);
        Assert.Equal(200, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturnFalseWhenEndDateIsLowerThanStartDate()
    {
        var request = new Request(1, DateTime.UtcNow.AddDays(2), DateTime.UtcNow);

        var result = await _handler.HandleAsync(request, CancellationToken.None);
        
        Assert.False(result.Success);
        Assert.Equal("Dados inválidos.", result.Message);
    }
}