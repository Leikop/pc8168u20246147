using pc8168u2024614.Domain.Model.Commands;
using pc8168u2024614.Domain.Model.ValueObjects;
using pc8168u2024614.Interfaces.REST.Resources;
using PC28168.U202416147.Openpay.Platform.Shared.Domain.Model.ValueObjects;

namespace pc8168u2024614.Interfaces.REST.Transform;

public static class CreateDisbursementOrderCommandFromResourceAssembler
{
    public static CreateDisbursementOrderCommand ToCommandFromResource(CreateDisbursementOrderResource resource) =>
        new(new OpenpayReference(resource.OrderTrackingCode!.Value),
            new StoreIdentifier(resource.AffiliateStoreId!.Value),
            new DateInterval(resource.ScheduledFrom!.Value, resource.ScheduledTo!.Value),
            new FinancialAmount(resource.TransferCapAmount!.Value, resource.TransferCapCurrencyCode),
            ToStatus(resource.Status),
            resource.User,
            resource.Remarks);

    private static DisbursementStatus ToStatus(string? status)
    {
        if (string.IsNullOrWhiteSpace(status)) return DisbursementStatus.Queued;

        return Enum.TryParse<DisbursementStatus>(status, true, out var result) && Enum.IsDefined(result)
            ? result
            : throw new ArgumentException("Status must be Queued, Processed, Cancelled or Failed.", nameof(status));
    }
}
