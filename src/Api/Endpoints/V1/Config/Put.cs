using Api.Endpoints.V1.Model;
using Api.Infrastructure;
using Api.Infrastructure.Contract;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints.V1.Config;

public class Put : IEndpoint
{
    private static async Task<IResult> Handler(
        [FromRoute] string itemId,
        [FromBody] ConfigRequestModel request,
        [FromServices] IConfigRepository configRepository,
        CancellationToken cancellationToken)
    {
        var entity = await configRepository.GetAsync(itemId, cancellationToken);
        if (entity == null)
        {
            entity = new ItemConfigEntity
            {
                ItemId = itemId,
                CreatedAt = DateTime.UtcNow
            };
        }

        entity.WorkingHours = request.WorkingHours;
        entity.SlotCountAtSameTime = request.SlotCountAtSameTime;
        entity.UpdatedAt = DateTime.UtcNow;

        await configRepository.SaveAsync(entity, cancellationToken);

        return Results.Ok();
    }

    public RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapPut("/v1/config/{itemId}", Handler)
            .Produces200()
            .Produces400()
            .Produces404()
            .Produces500()
            .WithTags("Config");
    }
}