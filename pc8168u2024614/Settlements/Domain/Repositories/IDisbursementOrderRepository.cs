using pc8168u2024614.Domain.Model.Aggregates;
using pc8168u2024614.Domain.Model.ValueObjects;
using pc8168u2024614.Openpay.Platform.Shared.Domain.Repositories;

namespace pc8168u2024614.Domain.Repositories;

public interface IDisbursementOrderRepository : IBaseRepository<DisbursementOrder>
{
    Task<bool> ExistsByOrderTrackingCodeAsync(OpenpayReference orderTrackingCode,
        CancellationToken cancellationToken = default);
}
