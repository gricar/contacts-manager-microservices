namespace BuildingBlocks.Messaging.Events;

public record DeleteContactEvent : IntegrationEvent
{
    public Guid ContactId { get; set; }
}