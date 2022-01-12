 using System;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using FinanceAccounting.Application.Common.Exceptions;
using FinanceAccounting.Domain.Exceptions.Base;
using FinanceAccounting.WebApi.ViewModels;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace FinanceAccounting.WebApi.Middleware
{
    public sealed class ExceptionHandlingMiddleware : IMiddleware
    {
        private const string _defaultErrorMessage = "An error occurred on the server side, please contact support";
        private const string _requestCancelledMessage = "Request was cancelled";

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;
            var result = new ErrorDetails { ErrorMessage = exception.Message };

            switch (exception)
            {
                case ValidationException validationException:
                    code = HttpStatusCode.BadRequest;
                    result.ErrorMessage = validationException.Errors.FirstOrDefault()?.ErrorMessage;
                    break;
                case NotFoundException:
                    code = HttpStatusCode.NotFound;
                    break;
                case BadRequestException:
                    code = HttpStatusCode.BadRequest;
                    break;
                case UserAuthenticationException:
                case TokenValidationException:
                    code = HttpStatusCode.Unauthorized;
                    break;
                case OperationCanceledException:
                    code = HttpStatusCode.BadRequest;
                    result.ErrorMessage = _requestCancelledMessage;
                    break;
                default:
                    result.ErrorMessage = _defaultErrorMessage;
                    break;
            }

            result.StatusCode = (int)code;
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = result.StatusCode;

            string response = JsonSerializer.Serialize(result);

            return context.Response.WriteAsync(response);
        }
    }
}
