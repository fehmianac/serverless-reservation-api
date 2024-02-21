using System.Text.Json.Serialization;
using Domain.Entities.Base;
using Domain.Extensions;

namespace Domain.Entities;

public class DailyReservationEntity : IEntity
{
    [JsonPropertyName("pk")] public string Pk => GetPk(ItemId);

    [JsonPropertyName("sk")] public string Sk => Date.ToString("yyyy-MM-dd");
    [JsonPropertyName("date")] public DateTime Date { get; set; }
    [JsonPropertyName("itemId")] public string ItemId { get; set; } = default!;

    [JsonPropertyName("items")] public List<string> Items { get; set; } = new();

    [JsonPropertyName("ttl")] public long Ttl => Date.AddYears(1).ToUnixTimeSeconds();
    public static string GetPk(string itemId) => "dailyReservation#" + itemId;
}