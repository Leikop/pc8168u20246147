using pc8168u2024614.Openpay.Platform.Shared.Domain.Model;

namespace pc8168u2024614.Domain.Model.Aggregates;

public partial class DisbursementOrder : IAuditableEntity
{
    public DateTimeOffset? CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }
}
