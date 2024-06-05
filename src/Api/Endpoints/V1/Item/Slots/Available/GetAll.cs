using Api.Infrastructure;
using Api.Infrastructure.Contract;
using Domain.Domain;
using Domain.Repositories;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints.V1.Item.Slots.Available;

public class GetAll : IEndpoint
{
    private static async Task<IResult> Handler(
        [FromRoute] string id,
        [FromServices] IConfigRepository configRepository,
        [FromServices] IReservationService reservationService,
        CancellationToken cancellationToken)
    {
        var config = await configRepository.GetAsync(id, cancellationToken);
        if (config == null)
        {
            return Results.NotFound();
        }

        var now = DateTime.UtcNow.AddHours(3); // Istanbul Time
        var maxDate = now.AddDays(30);
        var reservations = await reservationService.GetReservationsAsync(id, now, maxDate, cancellationToken);
        
        var slots = new List<SlotDto>();
        
        var currentDate = now.Date;
        while (currentDate <= maxDate)
        {
            var workingHours = config.WorkingHours.FirstOrDefault(q=> q.DayOfWeek == currentDate.DayOfWeek);
            if (workingHours == null)
            {
                slots.Add(new SlotDto
                {
                    Date = currentDate,
                    Slots = new List<SlotDto.SlotHourDto>()
                });   
                currentDate = currentDate.AddDays(1);
                continue;
            }
          
            var slotHours = new List<SlotDto.SlotHourDto>();
            while (workingHours.Open <= workingHours.Close)
            {
                var currentDayReservations = reservations.Where(q => q.StartDate.Date == currentDate);
                
                var reservationCountInThatSlot = currentDayReservations.Count(q => q.StartDate.TimeOfDay <= workingHours.Open && q.EndDate.TimeOfDay >= workingHours.Open);
                slotHours.Add(new SlotDto.SlotHourDto
                {
                    StartTime = workingHours.Open,
                    EndTime = workingHours.Open.Add(TimeSpan.FromMinutes(config.DurationMinutes)),
                    IsAvailable = (config.SlotCountAtSameTime > reservationCountInThatSlot) && currentDate.Add(workingHours.Open) > now,
                    RemainingSlotCount = config.SlotCountAtSameTime - reservationCountInThatSlot,
                });
                workingHours.Open = workingHours.Open.Add(TimeSpan.FromMinutes(config.DurationMinutes));
            }
            
            slots.Add(new SlotDto
            {
                Date = currentDate,
                Slots = slotHours
            });
            currentDate = currentDate.AddDays(1);
        }
        return Results.Ok(slots);
    }

    public RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapGet("/v1/item/{id}/slots/available", Handler)
            .Produces200<List<SlotDto>>()
            .Produces400()
            .Produces500()
            .WithTags("Slots");
    }
}