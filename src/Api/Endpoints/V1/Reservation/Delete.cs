using Api.Context;
using Api.Infrastructure;
using Api.Infrastructure.Contract;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints.V1.Reservation;

public class Delete : IEndpoint
{
    private static async Task<IResult> Handler(
        [FromRoute] string id,
        [FromServices] IApiContext apiContext,
        [FromServices] IReservationService reservationService,
        CancellationToken cancellationToken)
    {
        var reservation = await reservationService.GetReservationAsync(id, cancellationToken);
        if (reservation is null)
            return Results.NotFound();

        if (reservation.UserId != apiContext.CurrentUserId)
            return Results.Forbid();


        var result = await reservationService.DeleteReservationAsync(id, cancellationToken);
        return result ? Results.Ok() : Results.NotFound();
    }

    public RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapDelete("/v1/reservation/{id}", Handler)
            .Produces200()
            .Produces400()
            .Produces500()
            .WithTags("Reservation");
    }
}