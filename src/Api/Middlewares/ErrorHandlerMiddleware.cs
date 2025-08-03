using System.Net;
using System.Text.Json;
using Application.Exceptions;
using Domain.Dtos.Responses;

namespace Api.Middlewares;

public class ErrorHandlerMiddleware(ILogger<ErrorHandlerMiddleware> logger, RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception error)
        {
            HttpResponse response = context.Response;
            response.ContentType = "application/json";

            string message = "Something was wrong!";
            string errorCode = "500.02.999";

            HttpStatusCode statusCode = HttpStatusCode.BadRequest;

            switch (error)
            {
                case EmailNotFoundException:
                    errorCode = "400.01.001";
                    message = error.Message;
                    break;

                case EmailAlreadyExistException:
                    errorCode = "400.02.001";
                    message = error.Message;
                    break;

                case InvalidCredentialsException:
                    errorCode = "400.03.001";
                    message = error.Message;
                    break;

                case UnhandledException:
                    errorCode = "500.99.001";
                    message = error.Message;
                    break;

                default:
                    statusCode = HttpStatusCode.InternalServerError;
                    break;
            }

            response.StatusCode = (int)statusCode;

            logger.LogInformation("An error has ocurred: {Message}", error.Message);
            var result = JsonSerializer.Serialize(CreateResponseErrorModel(errorCode, message));
            await response.WriteAsync(result);
        }
    }

    private static ResponseError CreateResponseErrorModel(string errorCode, string message) =>
        new()
        {
            Code = errorCode,
            DateTime = DateTimeOffset.Now,
            Mesage = message,
        };
}
