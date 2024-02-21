using Api.Infrastructure;
using Api.Infrastructure.Contract;
using Domain.Domain;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints.V1.Config;

public class GetAll : IEndpoint
{
    private static async Task<IResult> Handler(
        [FromQuery] string ids,
        [FromServices] IConfigRepository configRepository,
        CancellationToken cancellationToken)
    {
        var idList = ids.Split(',').ToList();
        var configs = await configRepository.GetAllAsync(idList, cancellationToken);
        return Results.Ok(configs.Select(x => x.ToDto()).ToList());
    }

    public RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapGet("/v1/config", Handler)
            .Produces200<List<ItemConfigDto>>()
            .Produces400()
            .Produces500()
            .WithTags("Config");
    }
}