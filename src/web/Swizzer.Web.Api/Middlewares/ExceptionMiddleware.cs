using Microsoft.AspNetCore.Http;
using Swizzer.Shared.Exceptions;
using Swizzer.Shared.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Swizzer.Web.Api.Middlewares
{
    public class ExceptionMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (Exception ex)
            {
                await HandleException(ex, context);
            }
        }

        private async Task HandleException(Exception ex, HttpContext context)
        {
            var message = ErrorCodes.Unknow;
            var statusCode = HttpStatusCode.InternalServerError;

            switch (ex)
            {
                case SwizzerSerwerException swizzerEx:
                    message = swizzerEx.ErrorCode;
                    statusCode = GetStatusCode(swizzerEx);
                    break;
            }

            var errorDto = new ErrorDto
            {
                Exception = ex,
                ErrorCode = message
            };

            var json = SwizzerJsonSerializer.Serialize(errorDto);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            await context.Response.WriteAsync(json);
        }

        private HttpStatusCode GetStatusCode(SwizzerSerwerException swizzerEx)
        {
            switch(swizzerEx.ErrorCode)
            {
                case ErrorCodes.Unauthorized:
                    return HttpStatusCode.Unauthorized;
                default:
                    return HttpStatusCode.InternalServerError;
            }
        }
    }
}
