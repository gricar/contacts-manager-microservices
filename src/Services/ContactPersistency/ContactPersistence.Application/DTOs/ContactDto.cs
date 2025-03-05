namespace ContactPersistence.Application.DTOs;

public record ContactDto(
        string Name,
        int DDDCode,
        string Phone,
        string? Email = null);