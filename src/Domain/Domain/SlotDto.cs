namespace Domain.Domain;

public class SlotDto
{
    public string ItemId { get; set; } = default!;
    public DateTime Date { get; set; }
    public List<SlotHourDto> Slots { get; set; } = new();
    
    public bool HasAvailableSlot => Slots.Any(x => x.IsAvailable);
    public class SlotHourDto
    {
        public string Label
        {
            get
            {
                if (EndTime == null)
                {
                    return $"{StartTime:hh\\:mm}";
                }

                return $"{StartTime:hh\\:mm} - {EndTime:hh\\:mm}";
            }
        }

        public TimeSpan StartTime { get; set; }
        public TimeSpan? EndTime  { get; set; }
        
        public bool IsAvailable { get; set; }
        public int RemainingSlotCount { get; set; }
    }
}