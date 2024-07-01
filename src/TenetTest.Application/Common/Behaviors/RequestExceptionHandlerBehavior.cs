using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;

namespace TenetTest.Application.Common.Behaviors;

public class RequestExceptionHandlerBehavior<TRequest, TResponse>(ILogger<RequestExceptionHandlerBehavior<TRequest, TResponse>> logger)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : IErrorOr
{
    private readonly ILogger<RequestExceptionHandlerBehavior<TRequest, TResponse>> _logger = logger;

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Request: Unhandled Exception for Request {@Name} {@Request}, {@DateTimeUtc}", typeof(TRequest).Name, request, DateTime.UtcNow);

            var errors = new List<Error>
                {
                    Error.Failure(description: ex.Message)
                };

            return (dynamic)errors;
        }
    }
}