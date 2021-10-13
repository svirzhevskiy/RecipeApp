using System;
using System.Text.Json;
using System.Threading.Tasks;
using Application.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace WebApi.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private static readonly ILogger Log = Serilog.Log.ForContext<ExceptionMiddleware>();

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
                    var devMessage = $"\n EXCEPTION: {exception.Message} \n {exception.StackTrace}";
                    // ReSharper disable once TemplateIsNotCompileTimeConstantProblem
                    Log.Error(devMessage);

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