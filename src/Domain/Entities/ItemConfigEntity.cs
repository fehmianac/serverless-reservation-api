using System.Net;
using System.Text.Json.Serialization;
using Domain.Domain;
using Domain.Entities.Base;

namespace Domain.Entities;

public class ItemConfigEntity : IEntity
{
    [JsonPropertyName("pk")] public string Pk => GetPk();

    [JsonPropertyName("sk")] public string Sk => ItemId;
    [JsonPropertyName("itemId")] public string ItemId { get; set; } = default!;
    [JsonPropertyName("workingHours")] public List<ItemConfigWorkingHourModel> WorkingHours { get; set; } = new();
    [JsonPropertyName("slotCountAtSameTime")] public int SlotCountAtSameTime { get; set; }
    [JsonPropertyName("CreatedAt")] public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    [JsonPropertyName("UpdatedAt")] public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    [JsonPropertyName("durationMinutes")] public int DurationMinutes { get; set; }

    public static string GetPk() => "itemConfig";
}