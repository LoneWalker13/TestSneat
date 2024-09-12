using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using BusinessCourse_Application.Exceptions;

namespace BusinessCourse.Common.Filter
{
  public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
  {

    private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;

    public ApiExceptionFilterAttribute()
    {
      // Register known exception types and handlers.
      _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
            {
                { typeof(BadRequestException), HandleBadRequestException},
                { typeof(ConflictException), HandleConflictException},
                { typeof(NotFoundException), HandleNotFoundException}
            };

    }

    public override void OnException(ExceptionContext context)
    {
      HandleException(context);

      base.OnException(context);
    }

    private void HandleException(ExceptionContext context)
    {
      Type type = context.Exception.GetType();
      if (_exceptionHandlers.ContainsKey(type))
      {
        _exceptionHandlers[type].Invoke(context);
        return;
      }

      HandleUnknownException(context);
    }

    private void HandleBadRequestException(ExceptionContext context)
    {
      var exception = context.Exception as BadRequestException;
      var details = new ProblemDetails
      {
        Status = StatusCodes.Status400BadRequest,
        Title = "Bad Request.",
        Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
        Detail = exception.Message
      };

      context.Result = new BadRequestObjectResult(details);

      context.ExceptionHandled = true;
    }

    private void HandleConflictException(ExceptionContext context)
    {
      var exception = context.Exception as ConflictException;
      var details = new ProblemDetails
      {
        Status = StatusCodes.Status409Conflict,
        Title = "Conflict Request.",
        Type = "https://tools.ietf.org/html/rfc7231#section-6.5.8",
        Detail = exception.Message
      };

      context.Result = new ConflictObjectResult(details);

      context.ExceptionHandled = true;
    }

    private void HandleNotFoundException(ExceptionContext context)
    {
      var exception = context.Exception as NotFoundException;
      var details = new ProblemDetails
      {
        Status = StatusCodes.Status404NotFound,
        Title = "Request Not Found.",
        Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
        Detail = exception.Message
      };

      context.Result = new NotFoundObjectResult(details);

      context.ExceptionHandled = true;
    }

    private void HandleUnknownException(ExceptionContext context)
    {
      var details = new ProblemDetails
      {
        Status = StatusCodes.Status500InternalServerError,
        Title = context.Exception.Message ?? "An error occurred while processing your request.",
        Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1"
      };

      context.Result = new ObjectResult(details)
      {
        StatusCode = StatusCodes.Status500InternalServerError
      };

      context.ExceptionHandled = true;
    }
  }

}
