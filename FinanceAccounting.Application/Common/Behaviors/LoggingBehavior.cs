using System.Threading;
using System.Threading.Tasks;
using System.Text.Json;
using FinanceAccounting.Application.Abstractions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FinanceAccounting.Application.Common.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest
        : IRequest<TResponse>
    {
        private readonly ILogger _logger;
        private readonly ICurrentUserService _currentUserService;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger, ICurrentUserService currentUserService)
        {
            _logger = logger;
            _currentUserService = currentUserService;
        }

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            string requestName = typeof(TRequest).Name;
            int userId = _currentUserService.UserId;
            string jsonRequest = JsonSerializer.Serialize(request);

            _logger.LogInformation($"Request: {requestName} with userId={userId} | {jsonRequest}");

            return next();
        }
    }
}
