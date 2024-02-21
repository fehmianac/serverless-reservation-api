using Api.Infrastructure;
using Api.Infrastructure.Contract;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints.V1.Config;

public class Delete : IEndpoint
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

        await configRepository.DeleteAsync(itemId, cancellationToken);
        return Results.Ok();
    }

    public RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapDelete("/v1/config/{itemId}", Handler)
            .Produces200()
            .Produces400()
            .Produces404()
            .Produces500()
            .WithTags("Config");
    }
}