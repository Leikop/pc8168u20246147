using pc8168u2024614.Application.Errors;
using pc8168u2024614.Domain.Model.Aggregates;
using pc8168u2024614.Domain.Model.Commands;
using pc8168u2024614.Openpay.Platform.Shared.Application.Patterns;

namespace pc8168u2024614.Application.Services;

public interface IDisbursementOrderCommandService
{
    Task<Result<DisbursementOrder, CreateDisbursementOrderError>> Handle(CreateDisbursementOrderCommand command,
        CancellationToken cancellationToken = default);
}
