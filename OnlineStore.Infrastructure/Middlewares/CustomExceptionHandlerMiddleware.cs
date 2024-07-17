using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using OnlineStore.Infrastructure.Contracts;
using System.Net;
using System.Security.Authentication;

namespace OnlineStore.Infrastructure.Middlewares;

public class CustomExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<CustomExceptionHandlerMiddleware> _logger;

    public CustomExceptionHandlerMiddleware(RequestDelegate next,
        ILogger<CustomExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
            if (context.Response.StatusCode == (int)HttpStatusCode.NotFound)
            {
                await HandleExceptionAsync(context, new HttpNotFoundException());
            }
            else if (context.Response.StatusCode == (int)HttpStatusCode.BadRequest)
            {
                await HandleExceptionAsync(context, new HttpBadRequestException());
            }
        }

        catch (AppException ex)
        {
            _logger.LogError(ex, ex.InnerException?.Message != null ? ex.InnerException?.Message : ex.Message);
            await HandleExceptionAsync(context, ex);
        }
        catch (UnauthorizedAccessException ex)
        {
            _logger.LogError(ex, ex.InnerException?.Message != null ? ex.InnerException?.Message : ex.Message);
            var appException = new HttpUnAuthorizedException(ex.InnerException?.Message != null ? ex.InnerException?.Message : ex.Message);
            await HandleExceptionAsync(context, appException);
        }
        catch (AuthenticationException ex)
        {
            _logger.LogError(ex, ex.InnerException?.Message != null ? ex.InnerException?.Message : ex.Message);
            var appException = new HttpUnAuthorizedException(ex.InnerException?.Message != null ? ex.InnerException?.Message : ex.Message);
            await HandleExceptionAsync(context, appException);
        }
        catch (AccessViolationException ex)
        {
            _logger.LogError(ex, ex.InnerException?.Message != null ? ex.InnerException?.Message : ex.Message);
            var appException = new HttpForbiddenException(ex.InnerException?.Message != null ? ex.InnerException?.Message : ex.Message);
            await HandleExceptionAsync(context, appException);
        }

        catch (Exception ex)
        {
            _logger.LogError(ex, ex.InnerException?.Message != null ? ex.InnerException?.Message : ex.Message);
            var appException = new HttpInternalServerErrorException(ex.InnerException?.Message != null ? ex.InnerException?.Message : ex.Message);
            await HandleExceptionAsync(context, appException);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, AppException exception)
    {
        var apiResult = new ApiResult(false, exception.HttpStatusCode, null);
        apiResult.Error(exception.Message);
        await context.Response.WriteAsync(apiResult.ToString());
    }

}