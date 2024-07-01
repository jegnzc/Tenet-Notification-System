using Microsoft.AspNetCore.Builder;

using TenetTest.Infrastructure.Common.Middleware;

namespace TenetTest.Infrastructure;

public static class RequestPipeline
{
    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder builder)
    {
        builder.UseMiddleware<EventualConsistencyMiddleware>();

        return builder;
    }
}