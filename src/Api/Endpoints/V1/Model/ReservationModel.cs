using FluentValidation;

namespace Api.Endpoints.V1.Model;

public class ReservationModel
{
    public string ItemId { get; set; } = default!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string? Description { get; set; }

    public Dictionary<string, string> AdditionalData { get; set; } = new();
}

public class ReservationModelValidator : AbstractValidator<ReservationModel>
{
    public ReservationModelValidator()
    {
        
    }
}