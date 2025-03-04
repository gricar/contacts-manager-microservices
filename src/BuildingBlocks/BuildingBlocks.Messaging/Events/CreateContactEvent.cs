namespace BuildingBlocks.Messaging.Events;

public record CreateContactEvent : IntegrationEvent
{
    public string Name { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string? Email { get; set; }
}
