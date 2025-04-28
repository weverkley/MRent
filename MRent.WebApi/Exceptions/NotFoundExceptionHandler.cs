using Microsoft.AspNetCore.Diagnostics;
using MRent.Domain.Exceptions;
using MRent.WebApi.Models;

namespace MRent.WebApi.Exceptions
{
    internal sealed class NotFoundExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<NotFoundExceptionHandler> _logger;

        public NotFoundExceptionHandler(ILogger<NotFoundExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            var message = exception.Message;

            if (exception is not MotorcycleNotFoundException notFoundException)
            {
                return false;
            }

            message = "Moto não encontrada";

            _logger.LogError(
                notFoundException,
                "Exception occurred: {Message}",
                notFoundException.Message);

            httpContext.Response.StatusCode = StatusCodes.Status404NotFound;

            await httpContext.Response
                .WriteAsJsonAsync(new Retorno(message), cancellationToken);

            return true;
        }
    }
}
