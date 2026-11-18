using pc8168u2024614.Domain.Model.Commands;
using pc8168u2024614.Domain.Model.ValueObjects;
using pc8168u2024614.Openpay.Platform.Shared.Domain.Model.ValueObjects;

namespace pc8168u2024614.Domain.Model.Aggregates;

public partial class DisbursementOrder
{
    public const int UserMaxLength = 20;
    public const int RemarksMaxLength = 85;

    protected DisbursementOrder()
    {
        OrderTrackingCode = null!;
        AffiliateStoreId = null!;
        ExecutionSchedule = null!;
        TransferCap = null!;
        User = string.Empty;
    }

    public DisbursementOrder(CreateDisbursementOrderCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);

        if (string.IsNullOrWhiteSpace(command.User) || command.User.Trim().Length > UserMaxLength)
            throw new ArgumentException($"User is required and cannot exceed {UserMaxLength} characters.", nameof(User));

        var remarks = string.IsNullOrWhiteSpace(command.Remarks) ? null : command.Remarks.Trim();
        if (remarks is not null && remarks.Length > RemarksMaxLength)
            throw new ArgumentException($"Remarks cannot exceed {RemarksMaxLength} characters.", nameof(Remarks));

        OrderTrackingCode = command.OrderTrackingCode;
        AffiliateStoreId = command.AffiliateStoreId;
        ExecutionSchedule = command.ExecutionSchedule;
        TransferCap = command.TransferCap;
        Status = command.Status;
        User = command.User.Trim();
        Remarks = remarks;
    }

    public int Id { get; private set; }

    public OpenpayReference OrderTrackingCode { get; private set; }

    public StoreIdentifier AffiliateStoreId { get; private set; }

    public DateInterval ExecutionSchedule { get; private set; }

    public FinancialAmount TransferCap { get; private set; }

    public DisbursementStatus Status { get; private set; } = DisbursementStatus.Queued;

    public string User { get; private set; }

    public string? Remarks { get; private set; }
}
