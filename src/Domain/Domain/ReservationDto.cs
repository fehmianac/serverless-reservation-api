using Domain.Entities;

namespace Domain.Domain;

public class ReservationDto
{
    public string Id { get; set; } = default!;
    public string ItemId { get; set; } = default!;
    public string UserId { get; set; } = default!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string? Description { get; set; }
    
    public Dictionary<string, string> AdditionalData { get; set; } = new();
}

public static class ReservationDtoMapper
{
    public static ReservationDto ToDto(this ReservationEntity entity)
    {
        return new ReservationDto
        {
            Id = entity.Id,
            ItemId = entity.ItemId,
            UserId = entity.UserId,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            Description = entity.Description,
            AdditionalData = entity.AdditionalData
        };
    }
}