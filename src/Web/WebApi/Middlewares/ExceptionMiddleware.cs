using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Application.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace WebApi.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch(Exception exception)
            {
                var env = httpContext.RequestServices.GetService(typeof(IWebHostEnvironment)) as IWebHostEnvironment;

                string message = "Application error";
                short statusCode = 500;
                
                if (exception is AppException ex)
                {
                    message = ex.Message;
                    statusCode = ex.StatusCode;
                }
                else
                {
                    var devMessage = $"{exception.Message} \n {exception.StackTrace}";

                    if (env.IsDevelopment())
                    {
                        message = devMessage;
                    }
                }
                
                
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = statusCode;
                    
                await httpContext.Response.WriteAsync(JsonSerializer.Serialize(new
                {
                    message = message
                }));
            }
        }
    }
}