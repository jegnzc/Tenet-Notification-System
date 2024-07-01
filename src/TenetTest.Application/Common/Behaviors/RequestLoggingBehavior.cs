using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;

namespace TenetTest.Application.Common.Behaviors;

public class RequestLoggingBehavior<TRequest, TResponse>(ILogger<RequestLoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : IErrorOr
{
    private readonly ILogger<RequestLoggingBehavior<TRequest, TResponse>> _logger = logger;

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling request {@RequestName}, {@DateTimeUtc}", typeof(TRequest).Name, DateTime.UtcNow);

        var result = await next();

        if (result.IsError)
        {
            var errorDetails = string.Join("; ", result.Errors!.Select(e => $"Code: {e.Code}, Description: {e.Description}"));

            _logger.LogError("Request failure {@RequestName}, {@ErrorDetails}, {@DateTimeUtc}",
                typeof(TRequest).Name, errorDetails, DateTime.UtcNow);
        }

        _logger.LogInformation("Handled request {@RequestName}, {@DateTimeUtc}", typeof(TRequest).Name, DateTime.UtcNow);

        return result;
    }
}
