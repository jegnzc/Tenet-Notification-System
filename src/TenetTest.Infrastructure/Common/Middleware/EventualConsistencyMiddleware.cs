using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

using TenetTest.Domain.Common;
using TenetTest.Infrastructure.Common.Persistence;

namespace TenetTest.Infrastructure.Common.Middleware;

public class EventualConsistencyMiddleware(RequestDelegate _next, ILogger<EventualConsistencyMiddleware> _logger)
{
    public const string DomainEventsKey = "DomainEventsKey";

    // This middleware is responsible for handling eventual consistency in the application
    public async Task InvokeAsync(HttpContext context, IPublisher publisher, TenetTestDbContext dbContext)
    {
        var transaction = await dbContext.Database.BeginTransactionAsync();
        context.Response.OnCompleted(async () =>
        {
            try
            {
                if (context.Items.TryGetValue(DomainEventsKey, out var value) && value is Queue<IDomainEvent> domainEvents)
                {
                    while (domainEvents.TryDequeue(out var nextEvent))
                    {
                        await publisher.Publish(nextEvent);
                    }
                }

                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while processing domain events");
                throw;
            }
            finally
            {
                await transaction.DisposeAsync();
            }
        });

        await _next(context);
    }
}