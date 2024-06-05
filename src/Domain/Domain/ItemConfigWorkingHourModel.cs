using System.Text.Json.Serialization;

namespace Domain.Domain;

public class ItemConfigWorkingHourModel
{
    [JsonPropertyName("dayOfWeek")] public DayOfWeek DayOfWeek { get; set; } = default!;
    [JsonPropertyName("open")] public TimeSpan Open { get; set; } = default!;
    [JsonPropertyName("close")] public TimeSpan Close { get; set; } = default!;
}