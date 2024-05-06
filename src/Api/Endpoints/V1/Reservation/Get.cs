using Api.Infrastructure;
using Api.Infrastructure.Contract;
using Domain.Domain;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints.V1.Reservation;

public class Get : IEndpoint
{
    private static async Task<IResult> Handler(
        [FromRoute] string id,
        [FromServices] IReservationService reservationService,
        CancellationToken cancellationToken)
    {
        var reservation = await reservationService.GetReservationAsync(id, cancellationToken);
        if (reservation is null)
            return Results.NotFound();
        return Results.Ok(reservation);
    }
    public RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapGet("/v1/reservation/{id}", Handler)
            .Produces200<ReservationDto>()
            .Produces400()
            .Produces404()
            .Produces500()
            .WithTags("Reservation");
    }
}