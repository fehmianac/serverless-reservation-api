using Domain.Domain;

namespace Api.Endpoints.V1.Model;

public class ConfigRequestModel
{
    public List<ItemConfigWorkingHourModel> WorkingHours { get; set; } = new();

    public int SlotCountAtSameTime { get; set; }

    public int DurationMinutes { get; set; } = 30;
    
    public class ItemConfigWorkingHourModel
    {
        public DayOfWeek DayOfWeek { get; set; } = default!;
        public string Open { get; set; } = default!;
        public string Close { get; set; } = default!;
    }
}