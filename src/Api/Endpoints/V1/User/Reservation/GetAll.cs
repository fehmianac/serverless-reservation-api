using Api.Infrastructure;
using Api.Infrastructure.Contract;
using Domain.Domain;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints.V1.User.Reservation;

public class GetAll : IEndpoint
{
    private static async Task<IResult> Handler(
        [FromRoute] string id,
        [FromServices] IReservationService reservationService,
        CancellationToken cancellationToken)
    {
        var reservations = await reservationService.GetReservationsByUserIdAsync(id, cancellationToken);
        return Results.Ok(reservations);
    }

    public RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapGet("/v1/user/{id}/reservation", Handler)
            .Produces200<List<ReservationDto>>()
            .Produces400()
            .Produces500()
            .WithTags("User");
    }
}