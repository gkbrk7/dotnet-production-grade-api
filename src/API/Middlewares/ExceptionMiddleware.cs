using System;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;
using API.BLL.Utilities.Exceptions;
using API.BLL.Utilities.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _env;
        public ExceptionMiddleware(RequestDelegate next, IWebHostEnvironment env)
        {
            this._env = env;
            this._next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (System.Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var code = HttpStatusCode.InternalServerError;
            var errors = new ApiErrorResponse { StatusCode = (int)code };

            if (_env.IsDevelopment())
            {
                errors.Details = ex.StackTrace;
            }
            else
            {
                errors.Details = ex.Message;
            }

            switch (ex)
            {
                case ApplicationValidationException e:
                    errors.Message = e.Message;
                    errors.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                    break;
                default:
                    errors.Message = "Something went wrong!";
                    break;
            }

            var result = JsonConvert.SerializeObject(errors);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = errors.StatusCode;
            await context.Response.WriteAsync(result);

        }
    }
}