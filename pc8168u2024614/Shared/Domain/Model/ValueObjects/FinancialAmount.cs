namespace pc8168u2024614.Shared.Domain.Model.ValueObjects;

public record FinancialAmount
{
    public FinancialAmount(decimal amount, string? currencyCode)
    {
        if (amount < decimal.Zero)
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be greater than or equal to zero.");

        if (currencyCode is null)
            throw new ArgumentNullException(nameof(currencyCode), "CurrencyCode cannot be null.");

        if (string.IsNullOrWhiteSpace(currencyCode))
            throw new ArgumentException("CurrencyCode cannot be empty or whitespace.", nameof(currencyCode));

        Amount = amount;
        CurrencyCode = currencyCode.Trim().ToUpperInvariant();
    }

    public decimal Amount { get; init; }

    public string CurrencyCode { get; init; }

    public FinancialAmount MultiplyBy(decimal factor) => new(Amount * factor, CurrencyCode);
}
