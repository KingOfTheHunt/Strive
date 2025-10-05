using Flunt.Validations;
using Strive.Core.Abstractions;

namespace Strive.Core.ValueObjects;

public class ScheduledAt : ValueObject
{
    public DateTime? Date { get; private set; }

    protected ScheduledAt() {}

    public ScheduledAt(DateTime scheduledAt)
    {
        AddNotifications(new Contract<ScheduledAt>()
            .Requires()
            .IsGreaterThan(scheduledAt, DateTime.UtcNow, nameof(scheduledAt),
                "A data escolhida está no passado."));

        if (!IsValid)
            return;

        Date = scheduledAt;
    }
}