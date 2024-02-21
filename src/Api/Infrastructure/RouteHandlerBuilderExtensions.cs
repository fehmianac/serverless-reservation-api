using Microsoft.AspNetCore.Mvc;

namespace Api.Infrastructure;

public static class RouteHandlerBuilderExtensions
{
    public static RouteHandlerBuilder Produces200(this RouteHandlerBuilder builder, params string[] additionalContentTypes)
    {
        return builder.Produces(StatusCodes.Status200OK, null, null, additionalContentTypes);
    }
    public static RouteHandlerBuilder Produces204(this RouteHandlerBuilder builder, params string[] additionalContentTypes)
    {
        return builder.Produces(StatusCodes.Status204NoContent, null, null, additionalContentTypes);
    }
    
    public static RouteHandlerBuilder Produces200<TResponse>(this RouteHandlerBuilder builder, params string[] additionalContentTypes)
    {
        return builder.Produces(StatusCodes.Status200OK, typeof(TResponse), null, additionalContentTypes);
    }

    internal static RouteHandlerBuilder Produces400(this RouteHandlerBuilder builder, params string[] additionalContentTypes)
    {
        return builder.Produces(StatusCodes.Status400BadRequest, typeof(ProblemDetails), "application/problem+json", additionalContentTypes);
    }
    
    internal static RouteHandlerBuilder Produces404(this RouteHandlerBuilder builder, params string[] additionalContentTypes)
    {
        return builder.Produces(StatusCodes.Status404NotFound, null, null, additionalContentTypes);
    }

    internal static RouteHandlerBuilder Produces500(this RouteHandlerBuilder builder, params string[] additionalContentTypes)
    {
        return builder.Produces(StatusCodes.Status500InternalServerError, typeof(ProblemDetails), "application/problem+json", additionalContentTypes);
    }
}