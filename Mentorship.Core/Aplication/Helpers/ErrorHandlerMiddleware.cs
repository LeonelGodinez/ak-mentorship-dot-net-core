using Mentorship.Core.Aplication.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Mentorship.Core.Aplication.Helpers
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware (RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try {
                await _next(context);
            }
            catch (Exception error) {
                var response = context.Response;
                response.ContentType = "application/json";

                switch (error) { 
                    case BadRequestException e:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }
                // we can add loggin here 
                var errorResponse = new ErrorResponse() { Code = error.Message, Message = error.InnerException?.Message };
                var restRespose = new RestResponse() { Success = false, Data = errorResponse };
                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true
                };
                var result = JsonSerializer.Serialize(restRespose, options);
                 await response.WriteAsync(result);
            }

        }
    }
}
