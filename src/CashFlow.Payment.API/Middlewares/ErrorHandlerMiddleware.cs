using CashFlow.Sidecar;
using CashFlow.Sidecar.Exceptions;
using System.Net;

namespace CashFlow.BFF.API.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private static ILogger _logger;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> log)
        {
            _logger = log;
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                _logger.LogError(error, error.Message);
                var response = context.Response;
                response.ContentType = "application/json";

                response.StatusCode = error switch
                {
                    UnauthorizedAccessException e => (int)HttpStatusCode.Unauthorized,
                    ValidationException e => (int)HttpStatusCode.BadRequest,
                    NotFoundException e => (int)HttpStatusCode.NotFound,
                    BadRequestException e => (int)HttpStatusCode.BadRequest,
                    ForbiddenAccessException e => (int)HttpStatusCode.Forbidden,
                    _ => (int)HttpStatusCode.InternalServerError// unhandled error
                };

                await response.WriteAsync(new ErrorResponse()
                {
                    StatusCode = response.StatusCode,
                    Message = error.Message
                }.ToString());
            }
        }
    }
}
