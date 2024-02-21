using Domain.Domain;

namespace Api.Endpoints.V1.Model;

public class ConfigRequestModel
{
    public List<ItemConfigWorkingHourModel> WorkingHours { get; set; } = new();

    public int SlotCountAtSameTime { get; set; }
}