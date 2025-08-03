using Domain.Enums;
using Domain.Events;
using Domain.Models.Identity;

namespace Domain.AggregateRoots;

public class User : AggregateRoot
{
    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string FullName
    {
        get { return $"{FirstName} {LastName}"; }
    }

    public UserState State { get; set; }

    public string? PictureUrl { get; set; }

    // Identity navigation properties
    public virtual ICollection<UserClaim>? Claims { get; set; }
    public virtual ICollection<UserLogin>? Logins { get; set; }
    public virtual ICollection<UserToken>? Tokens { get; set; }
    public virtual ICollection<UserRole>? UserRoles { get; set; }

    protected User() { }

    public User(string email, string passwordHash)
    {
        Email = email;
        UserName = email;
        PasswordHash = passwordHash;
        State = UserState.Active;
        CreatedOn = DateTime.UtcNow;

        AddDomainEvent(new UserRegisteredDomainEvent(Id, Email));
    }

    public void Deactivate()
    {
        State = UserState.Inactive;
        AddDomainEvent(new UserDeactivatedDomainEvent(Id));
    }

    public void Activate()
    {
        State = UserState.Active;
        AddDomainEvent(new UserActivatedDomainEvent(Id));
    }

    public void ChangePassword(string newHash)
    {
        PasswordHash = newHash;
        AddDomainEvent(new PasswordChangedDomainEvent(Id));
    }

    public void AddRole(UserRole role)
    {
        if (!UserRoles.Contains(role))
        {
            UserRoles.Add(role);
            AddDomainEvent(new RoleAddedToUserDomainEvent(Id, role.Role.Name));
        }
    }

    public void RemoveRole(UserRole role)
    {
        if (UserRoles.Remove(role))
            AddDomainEvent(new RoleRemovedFromUserDomainEvent(Id, role.Role.Name));
    }
}
