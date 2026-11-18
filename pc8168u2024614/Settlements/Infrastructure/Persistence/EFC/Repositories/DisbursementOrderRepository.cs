using Microsoft.EntityFrameworkCore;
using pc8168u2024614.Domain.Model.Aggregates;
using pc8168u2024614.Domain.Model.ValueObjects;
using pc8168u2024614.Domain.Repositories;
using PC28168.U202416147.Openpay.Platform.Shared.Infrastructure.Persistence.EFC.Configuration;
using PC28168.U202416147.Openpay.Platform.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace pc8168u2024614.Infrastructure.Persistence.EFC.Repositories;

public class DisbursementOrderRepository(AppDbContext context)
    : BaseRepository<DisbursementOrder>(context), IDisbursementOrderRepository
{
    public async Task<bool> ExistsByOrderTrackingCodeAsync(OpenpayReference orderTrackingCode,
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<DisbursementOrder>()
            .AnyAsync(d => d.OrderTrackingCode == orderTrackingCode, cancellationToken);
    }
}
