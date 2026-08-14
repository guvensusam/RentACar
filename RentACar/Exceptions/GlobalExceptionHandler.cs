using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace RentACar.Exceptions;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var (statusCode, baslik) = exception switch
        {
            NotFoundException => (StatusCodes.Status404NotFound, "Kayıt bulunamadı"),
            ValidationException => (StatusCodes.Status400BadRequest, "Geçersiz istek"),
            ConflictException => (StatusCodes.Status409Conflict, "İşlem gerçekleştirilemedi"),
            UnauthorizedAccessException => (StatusCodes.Status403Forbidden, "Erişim yetkiniz yok"),
            _ => (StatusCodes.Status500InternalServerError, "Beklenmeyen bir hata oluştu")
        };

        var detay = exception.Message;

        if (statusCode == StatusCodes.Status500InternalServerError)
        {
            _logger.LogError(exception, "Beklenmeyen hata: {Path}", httpContext.Request.Path);
            detay = "Beklenmeyen bir hata oluştu.";
        }

        var problemDetails = new ProblemDetails()
        {
            Status = statusCode,
            Title = baslik,
            Detail = detay,
            Instance = httpContext.Request.Path
        };

        httpContext.Response.StatusCode = statusCode;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}
