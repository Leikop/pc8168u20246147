using System.Globalization;
using pc8168u2024614.Domain.Model.Aggregates;
using pc8168u2024614.Interfaces.REST.Resources;

namespace pc8168u2024614.Interfaces.REST.Transform;

public static class DisbursementOrderResourceFromEntityAssembler
{
    public static DisbursementOrderResource ToResourceFromEntity(DisbursementOrder entity) =>
        new(entity.Id,
            entity.OrderTrackingCode.ToString(),
            entity.AffiliateStoreId.ToString(),
            entity.ExecutionSchedule.ScheduledFrom.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
            entity.ExecutionSchedule.ScheduledTo.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
            entity.TransferCap.Amount,
            entity.TransferCap.CurrencyCode,
            entity.Status.ToString(),
            entity.User,
            entity.Remarks,
            entity.UpdatedAt);
}
