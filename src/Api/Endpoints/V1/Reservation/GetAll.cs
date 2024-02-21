using Api.Endpoints.V1.Model;
using Api.Infrastructure;
using Api.Infrastructure.Contract;
using Domain.Domain;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints.V1.Reservation;

public class GetAll : IEndpoint
{
    private static async Task<IResult> Handler(
        [FromQuery] string itemId,
        [FromQuery] DateTime? startDate,
        [FromQuery] DateTime? endDate,
        [FromServices] IReservationService reservationService,
        CancellationToken cancellationToken
    )
    {
        startDate ??= DateTime.UtcNow.AddDays(-30);
        endDate ??= DateTime.UtcNow;
        var reservations = await reservationService.GetReservationsAsync(itemId, startDate.Value, endDate.Value, cancellationToken);
        return Results.Ok(reservations);
    }

    public RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapGet("/v1/reservation", Handler)
            .Produces200<List<ReservationDto>>()
            .Produces400()
            .Produces500()
            .WithTags("Reservation");
    }
}