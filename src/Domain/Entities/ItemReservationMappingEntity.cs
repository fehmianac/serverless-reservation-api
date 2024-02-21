using System.Text.Json.Serialization;
using Domain.Entities.Base;
using Domain.Extensions;

namespace Domain.Entities;

public class ItemReservationMappingEntity : IEntity
{
    [JsonPropertyName("pk")] public string Pk => GetPk(ItemId);
    [JsonPropertyName("sk")] public string Sk => GetSk(ReservationId);
    [JsonPropertyName("itemId")] public string ItemId { get; set; } = default!;
    [JsonPropertyName("reservationId")] public string ReservationId { get; set; } = default!;
    [JsonPropertyName("userId")] public string UserId { get; set; } = default!;
    [JsonPropertyName("date")] public DateTime? Date { get; set; }

    [JsonPropertyName("ttl")] long? Ttl => Date?.AddYears(1).ToUnixTimeSeconds();
    public static string GetPk(string itemId) => "itemReservationMapping#" + itemId;

    public static string GetSk(string reservationId)
    {
       
        return reservationId;
    }
}