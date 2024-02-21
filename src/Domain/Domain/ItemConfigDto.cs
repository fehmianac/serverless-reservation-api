using Domain.Entities;

namespace Domain.Domain;

public class ItemConfigDto
{
    public string ItemId { get; set; } = default!;

    public List<ItemConfigWorkingHourModel> WorkingHours { get; set; } = new();
    
    public int SlotCountAtSameTime { get; set; }
}

public static class ConfigDtoMapper
{
    public static ItemConfigDto ToDto(this ItemConfigEntity entity) => new()
    {
        ItemId = entity.ItemId,
        WorkingHours = entity.WorkingHours,
        SlotCountAtSameTime = entity.SlotCountAtSameTime
    };
}