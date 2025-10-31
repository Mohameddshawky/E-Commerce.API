using Domain.Exceptions;
using Shared.Error_Models;

namespace E_Commerce.API.MiddleWares
{
    public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

        public GlobalExceptionHandlingMiddleware(RequestDelegate requestDelegate,ILogger<GlobalExceptionHandlingMiddleware> logger)
        {
           _next = requestDelegate;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
                if(context.Response.StatusCode==StatusCodes.Status404NotFound)
                {
                    await HandleNotFoundApiAsync(context );
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong==> :{ex.Message} ");
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleNotFoundApiAsync(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            var response = new ErrorDetails()
            {
                StatusCode =StatusCodes.Status404NotFound,
                ErrorMessage = $"The EndPoint with url {context.Request.Path} is not found "

            }.ToString();
            await context.Response.WriteAsync(response);

        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        { 
            context.Response.ContentType = "application/json";
            var response = new ErrorDetails()
            {
                ErrorMessage = ex.Message

            };
            //context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.StatusCode= ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                UnauthorizedException=> StatusCodes.Status401Unauthorized,
                ValidationException validationException =>HadleValidationException(response,validationException),                    
                _ => StatusCodes.Status500InternalServerError
            };

            response.StatusCode = context.Response.StatusCode;

            await context.Response.WriteAsync(response.ToString());

        }

        private int HadleValidationException(ErrorDetails response, ValidationException validationException)
        {
           
            response.Errors = validationException.Errors;
            return StatusCodes.Status400BadRequest; 

        }
    }
}
