using pc8168u2024614.Domain.Model.ValueObjects;
using PC28168.U202416147.Openpay.Platform.Shared.Domain.Model.ValueObjects;

namespace pc8168u2024614.Domain.Model.Commands;

public record CreateDisbursementOrderCommand(
    OpenpayReference OrderTrackingCode,
    StoreIdentifier AffiliateStoreId,
    DateInterval ExecutionSchedule,
    FinancialAmount TransferCap,
    DisbursementStatus Status,
    string? User,
    string? Remarks);
