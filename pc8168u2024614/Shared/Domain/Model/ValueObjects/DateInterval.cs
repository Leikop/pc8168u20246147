namespace pc8168u2024614.Shared.Domain.Model.ValueObjects;

public record DateInterval
{
    public DateInterval(DateOnly scheduledFrom, DateOnly scheduledTo)
    {
        if (scheduledTo <= scheduledFrom)
            throw new ArgumentException("ScheduledTo must be later than ScheduledFrom.", nameof(scheduledTo));

        ScheduledFrom = scheduledFrom;
        ScheduledTo = scheduledTo;
    }

    public DateOnly ScheduledFrom { get; init; }

    public DateOnly ScheduledTo { get; init; }

    public bool HasElapsed(DateOnly referenceDate) => referenceDate > ScheduledTo;
}
