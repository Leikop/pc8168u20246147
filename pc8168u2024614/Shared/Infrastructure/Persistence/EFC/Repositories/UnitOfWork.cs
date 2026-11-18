using pc8168u2024614.Shared.Domain.Repositories;
using pc8168u2024614.Shared.Infrastructure.Persistence.EFC.Configuration;

namespace pc8168u2024614.Shared.Infrastructure.Persistence.EFC.Repositories;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    public async Task CompleteAsync(CancellationToken cancellationToken = default)
    {
        await context.SaveChangesAsync(cancellationToken);
    }
}
