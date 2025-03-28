using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Exceptions;
using Shop.Domain.Exceptions;

namespace Shop.ExceptionHandlers
{
    public class CustomExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            switch (exception)
            {
                case ValidationException:
                    httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

                    await httpContext.Response.WriteAsJsonAsync(new ProblemDetails()
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Title = "Validation error"
                    }, cancellationToken);

                    return true;
                case NotFoundException:
                    httpContext.Response.StatusCode = StatusCodes.Status404NotFound;

                    await httpContext.Response.WriteAsJsonAsync(new ProblemDetails()
                    {
                        Status = StatusCodes.Status404NotFound,
                        Title = "Resource not found"
                    }, cancellationToken);

                    return true;
                case DomainException ex:
                    httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

                    await httpContext.Response.WriteAsJsonAsync(new ProblemDetails()
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Title = ex.Message
                    }, cancellationToken);

                    return true;
            }

            // Return false to continue with the default behavior
            // - or - return true to signal that this exception is handled
            return false;
        }
    }
}
