namespace pc8168u2024614.Domain.Model.ValueObjects;

public sealed record StoreIdentifier
{
    public StoreIdentifier(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("StoreIdentifier cannot be an empty GUID.", nameof(value));

        Value = value;
    }

    public Guid Value { get; }

    public override string ToString() => Value.ToString();
}
