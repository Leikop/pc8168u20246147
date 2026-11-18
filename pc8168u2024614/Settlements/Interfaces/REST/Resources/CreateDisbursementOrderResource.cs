using System.ComponentModel.DataAnnotations;

namespace pc8168u2024614.Interfaces.REST.Resources;

public record CreateDisbursementOrderResource(
    [Required(ErrorMessage = "RequiredOrderTrackingCode")]
    Guid? OrderTrackingCode,
    [Required(ErrorMessage = "RequiredAffiliateStoreId")]
    Guid? AffiliateStoreId,
    [Required(ErrorMessage = "RequiredScheduledFrom")]
    DateOnly? ScheduledFrom,
    [Required(ErrorMessage = "RequiredScheduledTo")]
    DateOnly? ScheduledTo,
    [Required(ErrorMessage = "RequiredTransferCapAmount")]
    decimal? TransferCapAmount,
    string? TransferCapCurrencyCode,
    string? Status,
    string? User,
    string? Remarks);
