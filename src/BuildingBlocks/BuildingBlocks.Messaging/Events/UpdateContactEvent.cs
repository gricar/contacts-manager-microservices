namespace BuildingBlocks.Messaging.Events;

public record UpdateContactEvent : IntegrationEvent
{
    public string Name { get; set; }
    public int DDDCode { get; set; }
    public string Phone { get; set; }
    public string? Email { get; set; }
}
