namespace BuildingBlocks.Messaging.Events;

public record DeleteContactEvent : IntegrationEvent
{
    public Guid Id { get; set; } // Identificador único do contato a ser excluído
}