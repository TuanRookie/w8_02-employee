using Microsoft.IdentityModel.Tokens;
using MISA.WebFresher.MF1773.Demo.Domain;
using MISA.WebFresher.MF1773.Demo.Domain.Resource;
using System.Net;
using System.Security.Authentication;

namespace MISA.WebFresher.MF1773.Demo.API
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            Console.WriteLine(exception);
            context.Response.ContentType = "apptication/json";
            if (exception is ConnectDbException connectDbException)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync(text: new BaseException()
                {
                    ErrorCode = connectDbException.ErrorCode,
                    UserMessage = Resource.Exception_ConnectDb,
                    DevMessage = exception.Message,
                    TraceId = context.TraceIdentifier,
                    MoreInfo = exception.HelpLink
                }.ToString() ?? ""
                );
            }
            else if (exception is NotFoundException notFoundException)
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                await context.Response.WriteAsync(text: new BaseException()
                {
                    ErrorCode = notFoundException.ErrorCode,
                    UserMessage = Resource.ExceptionNotFound,
                    DevMessage = exception.Message,
                    TraceId = context.TraceIdentifier,
                    MoreInfo = exception.HelpLink
                }.ToString() ?? ""
                );
            }
            else if (exception is ValidateException validateException)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync(text: new BaseException()
                {
                    ErrorCode = validateException.ErrorCode,
                    UserMessage = Resource.Exception_Valid,
                    DevMessage = exception.Message,
                    TraceId = context.TraceIdentifier,
                    MoreInfo = exception.HelpLink
                }.ToString() ?? ""
                );
            }
            else if (exception is ConflictException conflictException)
            {
                context.Response.StatusCode = StatusCodes.Status409Conflict;
                await context.Response.WriteAsync(text: new BaseException()
                {
                    ErrorCode = conflictException.ErrorCode,
                    UserMessage = exception.Message,
                    DevMessage = Resource.Exception_Conflict,
                    TraceId = context.TraceIdentifier,
                    MoreInfo = exception.HelpLink
                }.ToString() ?? ""
                );
            }
            else if (exception is AuthenticationException authenticationException)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync(text: new BaseException()
                {
                    UserMessage = Resource.UnauthorizedAccess,
                    DevMessage = exception.Message,
                    TraceId = context.TraceIdentifier,
                    MoreInfo = exception.HelpLink,
                }.ToString() ?? ""
                );
            }
            else if (exception is SecurityTokenException securityException)
            {
                context.Response.StatusCode = StatusCodes.Status408RequestTimeout;
                await context.Response.WriteAsync(text: new BaseException()
                {
                    UserMessage = Resource.Exception_Valid,
                    DevMessage = exception.Message,
                    TraceId = context.TraceIdentifier,
                    MoreInfo = exception.HelpLink,
                }.ToString() ?? ""
                );
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync(text: new BaseException()
                {
                    ErrorCode = context.Response.StatusCode,
                    UserMessage = Resource.Exception_Server,
                    DevMessage = exception.Message,
                    TraceId = context.TraceIdentifier,
                    MoreInfo = exception.HelpLink
                }.ToString() ?? ""
                );
            }
        }
    }
}
