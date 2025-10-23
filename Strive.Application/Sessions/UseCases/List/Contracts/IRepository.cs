namespace Strive.Application.Sessions.UseCases.List.Contracts;

public interface IRepository
{
    Task<IReadOnlyCollection<ResponseData>> GetScheduleWorkoutsByDate(int userId, DateTime startDate, DateTime endDate,
        CancellationToken cancellationToken);
}