using System.Text.Json.Serialization;
using Domain.Entities.Base;

namespace Domain.Entities;

public class ReservationEntity : IEntity
{
    [JsonPropertyName("pk")] public string Pk => GetPk();
    [JsonPropertyName("sk")] public string Sk => $"{Id}";
    [JsonPropertyName("id")] public string Id { get; set; } = default!;
    [JsonPropertyName("itemId")] public string ItemId { get; set; } = default!;
    [JsonPropertyName("userId")] public string UserId { get; set; } = default!;
    [JsonPropertyName("startDate")] public DateTime StartDate { get; set; }
    [JsonPropertyName("endDate")] public DateTime EndDate { get; set; }
    [JsonPropertyName("description")] public string? Description { get; set; }
    [JsonPropertyName("additionalData")] public Dictionary<string, string> AdditionalData { get; set; } = new();

    public static string GetPk() => "reservation";
}