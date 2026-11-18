namespace pc8168u2024614.Domain.Model.ValueObjects;

public sealed record OpenpayReference
{
    public OpenpayReference(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("OpenpayReference cannot be an empty GUID.", nameof(value));

        Value = value;
    }

    public Guid Value { get; }

    public override string ToString() => Value.ToString();
}
