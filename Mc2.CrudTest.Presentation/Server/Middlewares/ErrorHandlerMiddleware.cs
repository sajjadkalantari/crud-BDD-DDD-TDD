using Mc2.CrudTest.Presentation.Infrustructure.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Presentation.Server.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ErrorHandlerMiddleware(RequestDelegate next,
            ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
#if DEBUG
                error = error.InnerException ?? error; // just in debug mode
#endif
                var responseModel = new { succeeded = false, error = error?.Message};

                switch (error)
                {
                    case ApiException:
                        // custom application error
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case InvalidRequestBodyException e:
                        // validation error
                        response.StatusCode = (int)HttpStatusCode.BadRequest;                        
                        break;
                    case EntityNotFoundException:
                        // validation error
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        Debugger.Break(); // break debugger to know why this error occurred!
                        break;
                }
                var result = JsonSerializer.Serialize(responseModel);

                await response.WriteAsync(result);
            }
        }
    }
}
