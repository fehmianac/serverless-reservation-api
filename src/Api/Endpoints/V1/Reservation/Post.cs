using Api.Context;
using Api.Endpoints.V1.Model;
using Api.Infrastructure;
using Api.Infrastructure.Contract;
using Domain.Domain;
using Domain.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints.V1.Reservation;

public class Post : IEndpoint
{
    private static async Task<IResult> Handler(
        [FromBody] ReservationModel request,
        [FromServices] IApiContext apiContext,
        [FromServices] IReservationService reservationService,
        [FromServices] IValidator<ReservationModel> validator,
        CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return Results.ValidationProblem(validationResult.ToDictionary());
        
        var overlappingReservations = await reservationService.CheckOverlappingReservationsAsync(request.ItemId, request.StartDate, request.EndDate, cancellationToken);
        if (overlappingReservations)
            return Results.Conflict("Overlapping reservations found");
        
        var reservationDto = new ReservationDto
        {
            ItemId = request.ItemId,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            Description = request.Description,
            UserId = apiContext.CurrentUserId
        };
        await reservationService.CreateReservationAsync(reservationDto, cancellationToken);
        return Results.Ok();
    }

    public RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapPost("/v1/reservation", Handler)
            .Produces200()
            .Produces400()
            .Produces500()
            .WithTags("Reservation");
    }
}