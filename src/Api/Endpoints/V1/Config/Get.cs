using Api.Infrastructure;
using Api.Infrastructure.Contract;
using Domain.Domain;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints.V1.Config;

public class Get : IEndpoint
{
    private static async Task<IResult> Handler(
        [FromRoute] string itemId,
        [FromServices] IConfigRepository configRepository,
        CancellationToken cancellationToken)
    {
        var config = await configRepository.GetAsync(itemId, cancellationToken);
        if (config == null)
        {
            return Results.NotFound();
        }

        return Results.Ok(config.ToDto());
    }

    public RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapGet("/v1/config/{itemId}", Handler)
            .Produces200<ItemConfigDto>()
            .Produces400()
            .Produces404()
            .Produces500()
            .WithTags("Config");
    }
}