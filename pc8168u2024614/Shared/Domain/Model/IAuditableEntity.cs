namespace pc8168u2024614.Shared.Domain.Model;

public interface IAuditableEntity
{
    DateTimeOffset? CreatedAt { get; set; }

    DateTimeOffset? UpdatedAt { get; set; }
}
