using Domain.AggregateRoots;
using Domain.Models.Common;
using Microsoft.AspNetCore.Identity;

namespace Domain.Models.Identity;

public class UserLogin : IdentityUserLogin<int>, IAuditable
{
    public virtual User? User { get; set; }

    // Auditable

    public bool IsDeleted { get; set; }
    public DateTimeOffset CreatedOn { get; set; }
    public DateTimeOffset? UpdatedOn { get; set; }
    public int? CreatedById { get; set; }
    public User? CreatedBy { get; set; }
    public int? UpdatedById { get; set; }
    public User? UpdatedBy { get; set; }
}
