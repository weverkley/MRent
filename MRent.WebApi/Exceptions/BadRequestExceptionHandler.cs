using Microsoft.AspNetCore.Diagnostics;
using MRent.Domain.Exceptions;
using MRent.WebApi.Models;

namespace MRent.WebApi.Exceptions
{
    internal sealed class BadRequestExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<BadRequestExceptionHandler> _logger;

        public BadRequestExceptionHandler(ILogger<BadRequestExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            var message = exception.Message;

            if (exception is not MotorcycleValidationException && exception is not CourierValidationException && exception is not RentValidationException)
            {
                return false;
            }

            message = "Dados inválidos";

            _logger.LogError(
                exception,
                "Bad Request Exception occurred: {Message}",
                exception.Message);

            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

            await httpContext.Response
                .WriteAsJsonAsync(new Retorno(message), cancellationToken);

            return true;
        }
    }
}
