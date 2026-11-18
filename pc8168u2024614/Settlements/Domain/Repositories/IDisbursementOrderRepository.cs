using pc8168u2024614.Domain.Model.Aggregates;
using pc8168u2024614.Domain.Model.ValueObjects;
using PC28168.U202416147.Openpay.Platform.Shared.Domain.Repositories;

namespace pc8168u2024614.Domain.Repositories;

public interface IDisbursementOrderRepository : IBaseRepository<DisbursementOrder>
{
    Task<bool> ExistsByOrderTrackingCodeAsync(OpenpayReference orderTrackingCode,
        CancellationToken cancellationToken = default);
}
