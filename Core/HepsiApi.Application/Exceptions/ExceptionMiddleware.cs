﻿using EllipticCurve.Utils;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using SendGrid.Helpers.Errors.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace HepsiApi.Application.Exceptions
{
    /* InvokeAsync Bu metot, her HTTP isteği için çağrılır.
       HttpContext: Mevcut HTTP isteği ve yanıtı hakkında bilgi içerir.
       RequestDelegate next: Pipeline'daki bir sonraki middleware'i temsil eder.
    */
    public class ExceptionMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext httpContext, RequestDelegate next)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }


        }
        private static Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            int statusCode = GetStatusCode(exception);
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = statusCode;

            if (exception.GetType() == typeof(ValidationException))
                return httpContext.Response.WriteAsync(new ExceptionModel
                {
                    Errors = ((ValidationException)exception).Errors.Select(x => x.ErrorMessage),
                    StatusCode = StatusCodes.Status400BadRequest

                }.ToString());

            List<string> errors = new()
            {
                $"Hata Mesajı: {exception.Message}"
            };

            return httpContext.Response.WriteAsync(new ExceptionModel
            {
                Errors = errors,
                StatusCode = statusCode
            }.ToString());
        }
        private static int GetStatusCode(Exception exception) =>
            exception switch
            {
                BadRequestException => StatusCodes.Status400BadRequest,
                NotFoundException => StatusCodes.Status404NotFound,
                ValidationException => StatusCodes.Status422UnprocessableEntity,
                //default değeri
                _ => StatusCodes.Status500InternalServerError
            };

    }
}
