namespace Application.IntegrationEvents;

public record UserRegisteredIntegrationEvent(int UserId, string Email);
