using Domain.AggregateRoots;

namespace Domain.Models.Common;

public interface IAuditable
{
    bool IsDeleted { get; set; }

    DateTimeOffset CreatedOn { get; set; }

    DateTimeOffset? UpdatedOn { get; set; }

    int? CreatedById { get; set; }

    User? CreatedBy { get; set; }

    int? UpdatedById { get; set; }

    User? UpdatedBy { get; set; }
}
