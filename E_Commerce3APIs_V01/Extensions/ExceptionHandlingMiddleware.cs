﻿using System;
using System.Net;
using System.Threading.Tasks;
using E_Commerce2Business_V01.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace E_Commerce3APIs_V01.Extensions
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); // Call the next middleware
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex); // Handle the exception
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            if (ex is BadRequestException badRequestEx)
            {
                _logger.LogError(badRequestEx, "An unexpected error occurred.");
            }
            else
                // Log the exception details
                _logger.LogError(ex, "An unexpected error occurred.");
            // Set the response code based on the exception type
            var responseCode = (int)HttpStatusCode.InternalServerError; // Default to 500
            switch (ex)
            {
                case NotFoundException _:
                    responseCode = (int)HttpStatusCode.NotFound; // Change to 404
                    break;
                case ConflictException _:
                    responseCode = (int)HttpStatusCode.Conflict; // Change to 409
                    break;
                case BadRequestException _:
                    {
                        responseCode = (int)HttpStatusCode.BadRequest; // Change to 400
                    }
                    break;
                case InternalServerErrorException _:
                    responseCode = (int)HttpStatusCode.InternalServerError; // Change to 500
                    break;
                case UnauthorizedAccessException _:
                    responseCode = (int)HttpStatusCode.Unauthorized; // Change to 401
                    break;

            }
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = responseCode;
            var response = new
            {
                StatusCode = responseCode,
                Message = ex.Message, // Provide the error message
                                      // Optionally include more details (stack trace, etc.)
            };
            return context.Response.WriteAsync(JsonConvert.SerializeObject(response)); // Serialize and send the response
        }
    }
}

