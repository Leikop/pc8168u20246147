using Microsoft.EntityFrameworkCore;
using pc8168u2024614.Application.Errors;
using pc8168u2024614.Application.Services;
using pc8168u2024614.Domain.Model.Aggregates;
using pc8168u2024614.Domain.Model.Commands;
using pc8168u2024614.Domain.Repositories;
using pc8168u2024614.Openpay.Platform.Shared.Application.Patterns;
using pc8168u2024614.Openpay.Platform.Shared.Domain.Repositories;
using pc8168u2024614.Shared.Domain.Repositories;
using pc8168u20246147.Shared.Application.Patterns;

namespace pc8168u2024614.Application.Internal.CommandServices;

public class DisbursementOrderCommandService(
    IDisbursementOrderRepository disbursementOrderRepository,
    IUnitOfWork unitOfWork,
    ILogger<DisbursementOrderCommandService> logger)
    : IDisbursementOrderCommandService
{
    public async Task<Result<DisbursementOrder, CreateDisbursementOrderError>> Handle(
        CreateDisbursementOrderCommand command, CancellationToken cancellationToken = default)
    {
        if (await disbursementOrderRepository.ExistsByOrderTrackingCodeAsync(command.OrderTrackingCode, cancellationToken))
        {
            logger.LogWarning("Duplicate disbursement order rejected for OrderTrackingCode {OrderTrackingCode}",
                command.OrderTrackingCode);
            return new Result<DisbursementOrder, CreateDisbursementOrderError>.Failure(
                CreateDisbursementOrderError.DuplicateOrderTrackingCode);
        }

        var disbursementOrder = new DisbursementOrder(command);

        try
        {
            await disbursementOrderRepository.AddAsync(disbursementOrder, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return new Result<DisbursementOrder, CreateDisbursementOrderError>.Success(disbursementOrder);
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex, "Database update failed creating disbursement order {OrderTrackingCode}",
                command.OrderTrackingCode);
            return new Result<DisbursementOrder, CreateDisbursementOrderError>.Failure(
                CreateDisbursementOrderError.UnexpectedError);
        }
    }
}
