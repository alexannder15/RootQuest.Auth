using Domain.AggregateRoots;

namespace Domain.Models.Common;

public class Auditable : EntityBase, IAuditable
{
    public DateTimeOffset CreatedOn { get; set; }

    public DateTimeOffset? UpdatedOn { get; set; }

    public int? CreatedById { get; set; }

    public User? CreatedBy { get; set; }

    public int? UpdatedById { get; set; }

    public User? UpdatedBy { get; set; }
}
