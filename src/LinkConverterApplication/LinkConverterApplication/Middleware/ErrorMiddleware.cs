using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using LinkConverterApplication.Entities;
using LinkConverterApplication.Repositories;

namespace LinkConverterApplication.Middleware
{
    public class ErrorMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IUnitOfWork _unitOfWork;

        public ErrorMiddleware(RequestDelegate next, IUnitOfWork unitOfWork)
        {
            _next = next;
            _unitOfWork = unitOfWork;
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

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var remoteIpAddress = context.Connection.RemoteIpAddress?.MapToIPv4().ToString();
            
            if (context.Request.Headers.ContainsKey("X-Forwarded-For"))
            {
                remoteIpAddress = context.Request.Headers["X-Forwarded-For"];
            }

            var error = new Error
            {
                Message = ex.Message,
                Stacktrace = ex.StackTrace,
                ProjectName = "NttDataLinkConverterApplication",
                CreatedOn = DateTime.Now,
                IpAddress = remoteIpAddress,
            };

            _unitOfWork.Error.InsertAsync(error);

            var result = JsonConvert.SerializeObject(
                new
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    IsSuccess = false,
                    Message = "There is a error please check your request."
                });

            return context.Response.WriteAsync(result);
        }
    }
}